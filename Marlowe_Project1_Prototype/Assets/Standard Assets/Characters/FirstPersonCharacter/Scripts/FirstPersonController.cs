using System;
using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;


namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] public MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip[] m_LeafFootsteps;
        [SerializeField] private AudioClip[] m_WoodFootsteps;
        [SerializeField] private AudioClip[] m_FlooringFootsteps;
        [SerializeField] public AudioClip hit_sound;
        [SerializeField] private AudioClip kunai_throw_sound;
        [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.

        public Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        public CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private string surface;    //default surface, leaves, wood, flooring, rocks
		    public bool started = false;
        public AudioSource m_AudioSource;

        public int health = 3;
        public float invincibility_time = 2.0f;
        public float invincibility_timer = 2.0f;
        

        public int kunaiCount = 0;
        public bool showKunai = false;
        public GameObject kunaiHand;
        public GameObject kunai;
        public GameObject throwSpot;
        public float kunai_threshold = 0.5f;
        public float kunai_time_passed = 0.5f;


    // Use this for initialization
    private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            //m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
			m_MouseLook.Init(transform , m_Camera.transform);
        }


        // Update is called once per frame
        private void Update()
        {
            invincibility_timer += Time.deltaTime;
            kunai_time_passed += Time.deltaTime;

            RotateView();
            // the jump state needs to read here to make sure it is not missed
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

          if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;

          if (health <= 0)
          {
                //some death thing shows up
                GetComponent<Animator>().Play("DeathAnim");
                m_MouseLook.SetCursorLock(false);

                  SceneManager.LoadScene("GameOverScreen");
            }

          if (kunaiCount > 0)
          {
            //if no kunai, but now just getting a kunai, show the kunai raise animation
            if (!showKunai)
            {
              showKunai = true;
              kunaiHand.SetActive(true);
              

            }
          }
          else
          {
            showKunai = false;
            kunaiHand.SetActive(false);
          }

        }


        private void PlayLandingSound()
        {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }


        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x*speed;
            m_MoveDir.z = desiredMove.z*speed;

          if (m_MoveDir.x != 0 || m_MoveDir.z != 0)
          {
            if (showKunai == true)
            {
              if (kunaiHand.GetComponent<Animator>())
              {
                kunaiHand.GetComponent<Animator>().enabled = true;
                kunaiHand.GetComponent<Animator>().Play("KunaiHandAnim");
              }
            }
          }
          else
          {
        if (kunaiHand.GetComponent<Animator>())
        {
          kunaiHand.GetComponent<Animator>().enabled = false;
        }
      }

            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)
                {
                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }

              
            }
            else
            {
                m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);

            m_MouseLook.UpdateCursorLock();
        }


        private void PlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }


        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0

            if(surface == "leaves")
            {
                int n = Random.Range(1, m_LeafFootsteps.Length);
                m_AudioSource.clip = m_LeafFootsteps[n];
                m_AudioSource.PlayOneShot(m_AudioSource.clip);
                // move picked sound to index 0 so it's not picked next time
                m_LeafFootsteps[n] = m_LeafFootsteps[0];
                m_LeafFootsteps[0] = m_AudioSource.clip;
            }
            else if(surface == "wood")
            {

            }
            else if(surface == "flooring")
            {

            }
            else
            {
              //happens only on a default surface  
              int n = Random.Range(1, m_FootstepSounds.Length);
              m_AudioSource.clip = m_FootstepSounds[n];
              m_AudioSource.PlayOneShot(m_AudioSource.clip);
              // move picked sound to index 0 so it's not picked next time
              m_FootstepSounds[n] = m_FootstepSounds[0];
              m_FootstepSounds[0] = m_AudioSource.clip;
            }
            
        }


        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed*(m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }

          if (Input.GetMouseButtonDown(0) && kunaiCount > 0 && kunai_time_passed > kunai_threshold)
          {
            kunaiCount--;
            ThrowKunai();
            kunai_time_passed = 0.0f;
            kunaiHand.GetComponent<Animator>().enabled = false;
      }
        }


        private void RotateView()
        {
            m_MouseLook.LookRotation (transform, m_Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
              if(surface != hit.collider.gameObject.tag)
              {
                  //change the object im walking on if it's different from what I'm currently on
                  surface = hit.collider.gameObject.tag;
              }
                return;
            }
            else
            {
                Debug.Log("Collision flags turned on: " + m_CollisionFlags);

                if ((hit.collider.gameObject.tag == "guard" || hit.collider.gameObject.tag == "head") && hit.collider.gameObject.GetComponent<GuardLogic>().dead == false && invincibility_timer > invincibility_time)
                {
                    //knock body back
                    gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-m_CharacterController.velocity * 0.5f, hit.point, ForceMode.Impulse);
                    health = health - 1;
                    m_AudioSource.clip = hit_sound;
                    m_AudioSource.Play();
                    invincibility_timer = 0f;

                }
              else if (hit.collider.gameObject.tag == "arrow" && invincibility_timer > invincibility_time)
                {
                  gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-m_CharacterController.velocity * 0.5f, hit.point, ForceMode.Impulse);
                  m_AudioSource.clip = hit_sound;
                  m_AudioSource.Play();
                  invincibility_timer = 0f;
                }
                
            }

            if (body == null || body.isKinematic)
            {
                

                Debug.Log("Character collided with " + hit.collider.gameObject.name);
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
			
			
			
        }

      public void ThrowKunai()
      {
        //Vector3 kunaiDir = -m_Camera.gameObject.transform.forward;
        Vector3 kunaiDir = new Vector3(m_Camera.transform.forward.x, 90, -m_Camera.transform.forward.z);

      GameObject kunaiOBJ = Instantiate(kunai, throwSpot.transform.position, throwSpot.transform.rotation);

            m_AudioSource.clip = kunai_throw_sound;
            m_AudioSource.Play();
            kunaiOBJ.GetComponent<Rigidbody>().AddForce(throwSpot.transform.forward * 125, ForceMode.VelocityChange);

            Debug.Log("Thrown knai directionP: " + kunaiDir);
            Debug.Log("Camera kunai_forward_vector: " + m_Camera.transform.forward);

            //m_Camera.transform.rotation.y
            Debug.Log("Throwing Kunai");
        kunaiHand.GetComponent<Animator>().enabled = true;
          kunaiHand.GetComponent<Animator>().Play("KunaiThrow");
    }


        private IEnumerator WaitForAnimation(Animation animation)
        {
            do
            {
                yield return null;
            } while (animation.isPlaying);
        }
    }

    
}
