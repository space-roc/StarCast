using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This is used for the ingame input.
public class InputManager : MonoBehaviour
{
    
// TEMP: THIS IS FOR SIMULATION SCENE ONLY
  //  public SpacecraftInput spacecraftInput; // this has moved to SettingsDataContainer_ScriptableObject

    private static int IM_controllerMethod;
    private static bool S_controllerHUD_alreadyPushed = false;
    private static bool S_cameraToggle_alreadyPushed = false;
    private static bool S_diagnosticWindowAlreadyPushed = false;
    private static bool S_toggleChooseDifficultyHeld = false;
    
    private static bool S_VERBOSE = false;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log(" AWAKE InputManager");

    
    }
/*
    // This returns true if we are using the gamepad and we are using the old input mananger (WEBGL safe)
    public static bool useOldInputAndGamepad(string _scriptName)
    {
        if(!Vars.newInputSystem && checkIfGamepadIsController()) 
        {
            //Debug.Log(" useOldInputAndGamepad() input recorded in script: "+ _scriptName);
            return true;
        } else {
            return false;
        }
    }

    public static void disableAllActionMaps(SpacecraftInput _inputAssetObj)
    {
        // ARGH CANT GET IT TO LOOP OVER ACTION MAPS, ONLY ACTIONS!

   
         _inputAssetObj.menuScreen.Enable();
         _inputAssetObj.flySpacecraft.Disable(); 
         _inputAssetObj.flySpacecraft_LH.Disable(); 
         _inputAssetObj.roboticArm.Disable(); 
         _inputAssetObj.roboticArm_LH.Disable(); 
         _inputAssetObj.enterScoreScreen.Disable();
        //Debug.Log(" Action map disabled: " +_inputAssetObj.enterScoreScreen);
    }

    public static void checkActionMapStatus(SpacecraftInput _inputAssetObj)
    {
        checkActionMap(_inputAssetObj.menuScreen.enabled, "menuScreen");
        checkActionMap(_inputAssetObj.flySpacecraft.enabled, "flySpacecraft");
        checkActionMap(_inputAssetObj.flySpacecraft_LH.enabled, "flySpacecraft_LH");
        checkActionMap(_inputAssetObj.roboticArm.enabled, "roboticArm");
        checkActionMap(_inputAssetObj.roboticArm_LH.enabled, "roboticArm_LH");
        checkActionMap(_inputAssetObj.enterScoreScreen.enabled, "enterScoreScreen");

    }

    public static void checkActionMap(bool _state, string _stateName)
    {
        if(_state)
        {
            Debug.Log(_stateName+" ON");
        } else {
            Debug.Log(_stateName+" OFF");
        }
    }
    
    public static void enableActionMap(SpacecraftInput _inputAssetObj, InputActionMap _actionMap)
    {   
        disableAllActionMaps(_inputAssetObj);
        
        _actionMap.Enable();
        //Debug.Log(" Action map enabled: " +_actionMap);
    }

    public static bool actionMapEnabled(InputActionMap _actionMap)
    {   
        if(_actionMap.enabled)
        {
            return true;
        } else {
            return false;
        }
    }

    public static bool actionMapDisabled(InputActionMap _actionMap)
    {   
        if(_actionMap.enabled)
        {
            return false;
        } else {
            return true;
        }
    }
    public static void centreCursor (bool hideCursor, bool lockCursor)
    {
        // Place cursor in middle of the screen and remove mouse cursor at start of game
        Vector2 screenCentre;
        screenCentre.x = 0.5f*(float)Screen.width;
        screenCentre.y = 0.5f*(float)Screen.height;
        Mouse.current.WarpCursorPosition(screenCentre);
        Cursor.visible = hideCursor;
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        } else {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public static void releaseCursorAndMakeVisible ()
    {
         Cursor.visible = true;
         Cursor.lockState = CursorLockMode.None;
    }
   
    public static void releaseCursorMakeInvisible ()
    {
         Cursor.visible = false;
         Cursor.lockState = CursorLockMode.None;
    }

    // When inputManager starts, need to determine the type of controller:
        // 1 = Keyboard only (mouse and controller off)
        // 2 = Keyboard and mouse (controller off)
        // 3 = Gamepad (keyboard short-cuts still work but mouse off in-game)
    public static int getControllerMethod()
    {
        if(IM_controllerMethod <1|| IM_controllerMethod >3 ) // got to default if not defined
        {
            IM_controllerMethod = Constants.KeyboardAndMouse;
            if(S_VERBOSE)Debug.Log("InputManager: No controlled method set - setting to KeyboardAndMouse as default");
        }
        return IM_controllerMethod;
    }

    public static bool checkIfGamepadIsController()
    {
        if(IM_controllerMethod ==3) 
        {
            return true;
        } else {
            return false;
        }
    }


    public static string getControllerMethodName()
    {
        string IM_controllerMethod_text;
        if(IM_controllerMethod == Constants.Keyboard)
        {
            IM_controllerMethod_text = "Keyboard only";    
        } else if (IM_controllerMethod == Constants.KeyboardAndMouse){

            IM_controllerMethod_text = "Keyboard and Mouse";
        } else if (IM_controllerMethod == Constants.GamePad){
            IM_controllerMethod_text = "Gamepad";
        } else {
            // Undefined controller  -set as keyboard and mouse
            IM_controllerMethod = Constants.KeyboardAndMouse;
            IM_controllerMethod_text = "Keyboard and Mouse";
        }
        return IM_controllerMethod_text;
    }
    
    // This needs to be set
    public static void setControllerMethod(int _controllerMethod)
    {
        IM_controllerMethod = _controllerMethod;
        // Save this in the player prefs
        PlayerPrefs.SetInt("controllerMethod",IM_controllerMethod);
    }
    
 // This needs to be set
    public static bool gamepadShortcutsOn()
    {
        if(IM_controllerMethod == Constants.GamePad)
        {
            return true;
        } else {
            return false;
        }
    }


    // Old input manager function assignments
    
    public static bool Gamepad_ButtonA()=> Input.GetButtonDown("A_button");

    public static bool Gamepad_ButtonB()=> Input.GetButtonDown("B_button");
    public static bool Gamepad_ButtonX()=> Input.GetButtonDown("X_button");
    public static bool Gamepad_ButtonY()=> Input.GetButtonDown("Y_button");
    public static bool Gamepad_ButtonLS()=> Input.GetButtonDown("LS_button"); // [4] left shoulder
    public static bool Gamepad_ButtonRS()=> Input.GetButtonDown("RS_button"); // [5] right shoulder
    public static bool Gamepad_ButtonView()=> Input.GetButtonDown("View_button");// [6]
    public static bool Gamepad_ButtonMenu()=> Input.GetButtonDown("Menu_button");// [7]
    public static bool Gamepad_ButtonLTS()=> Input.GetButtonDown("LT_button");// [8] Left thumbstick button
    public static bool Gamepad_ButtonRTS()=> Input.GetButtonDown("RT_button"); // [9] Right thumbstick button


    public static bool Gamepad_ButtonLS_Hold()=> Input.GetButton("LS_button"); // [4] left shoulder hold
    public static bool Gamepad_ButtonRS_Hold()=> Input.GetButton("RS_button"); // [5] right shoulder hold
    
    public static float Gamepad_Triggers() => Input.GetAxis("Triggers"); // Axis[3]: Left trigger = -1; Right trigger = 1; Both = 0;
    
    
    public static float Xbox_A_KeyPressed(SpacecraftInput _inputAssetObj, bool _leftHandedControls)
    {
        float Xbox_A_KeyPressed_Float = 0.0f;
        
        if(InputManager.useOldInputAndGamepad("InputManager.cs: Xbox_A_KeyPressed_Float"))
        {
            if(InputManager.Gamepad_ButtonA())
            {
                Xbox_A_KeyPressed_Float = 1.0f;
            } else {
                Xbox_A_KeyPressed_Float = 0.0f;
            }
        } else {
            if(_leftHandedControls){
                if(_inputAssetObj.flySpacecraft_LH.enabled)
                {
                    Xbox_A_KeyPressed_Float =  _inputAssetObj.flySpacecraft_LH.ButtonA.ReadValue<float>();   
                } else if(_inputAssetObj.roboticArm_LH.enabled){
                    Xbox_A_KeyPressed_Float =  _inputAssetObj.roboticArm_LH.ButtonA.ReadValue<float>();   
                }
            } else {
                if(_inputAssetObj.flySpacecraft.enabled)
                {
                    Xbox_A_KeyPressed_Float =  _inputAssetObj.flySpacecraft.ButtonA.ReadValue<float>();   
                } else if(_inputAssetObj.roboticArm.enabled){
                    Xbox_A_KeyPressed_Float =  _inputAssetObj.roboticArm.ButtonA.ReadValue<float>();   
                }      
            }      
        }
        return Xbox_A_KeyPressed_Float;
    }
    
    public static float Xbox_B_KeyPressed(SpacecraftInput _inputAssetObj, bool _leftHandedControls)
    {
        float Xbox_B_KeyPressed_Float = 0.0f;
        
        if(InputManager.useOldInputAndGamepad("InputManager.cs: Xbox_B_KeyPressed_Float"))
        {
            if(InputManager.Gamepad_ButtonB())
            {
                Xbox_B_KeyPressed_Float = 1.0f;
            } else {
                Xbox_B_KeyPressed_Float = 0.0f;
            }
        } else {
            if(_leftHandedControls){
                if(_inputAssetObj.flySpacecraft_LH.enabled)
                {
                    Xbox_B_KeyPressed_Float =  _inputAssetObj.flySpacecraft_LH.ButtonB.ReadValue<float>();   
                } else if(_inputAssetObj.roboticArm_LH.enabled){
                    Xbox_B_KeyPressed_Float =  _inputAssetObj.roboticArm_LH.ButtonB.ReadValue<float>();   
                }
            } else {
                if(_inputAssetObj.flySpacecraft.enabled)
                {
                    Xbox_B_KeyPressed_Float =  _inputAssetObj.flySpacecraft.ButtonB.ReadValue<float>();   
                } else if(_inputAssetObj.roboticArm.enabled){
                    Xbox_B_KeyPressed_Float =  _inputAssetObj.roboticArm.ButtonB.ReadValue<float>();   
                }      
            }       
        }
        return Xbox_B_KeyPressed_Float;
    }
    
      public static float Xbox_X_KeyPressed(SpacecraftInput _inputAssetObj, bool _leftHandedControls)
    {
        float Xbox_X_KeyPressed_Float = 0.0f;
        
        if(InputManager.useOldInputAndGamepad("InputManager.cs: Xbox_B_KeyPressed_Float"))
        {
            if(InputManager.Gamepad_ButtonB())
            {
                Xbox_X_KeyPressed_Float = 1.0f;
            } else {
                Xbox_X_KeyPressed_Float = 0.0f;
            }
        } else {
            if(_leftHandedControls){
                if(_inputAssetObj.flySpacecraft_LH.enabled)
                {
                    Xbox_X_KeyPressed_Float =  _inputAssetObj.flySpacecraft_LH.ButtonX.ReadValue<float>();   
                } else if(_inputAssetObj.roboticArm_LH.enabled){
                    Xbox_X_KeyPressed_Float =  _inputAssetObj.roboticArm_LH.ButtonX.ReadValue<float>();   
                }
            } else {
                if(_inputAssetObj.flySpacecraft.enabled)
                {
                    Xbox_X_KeyPressed_Float =  _inputAssetObj.flySpacecraft.ButtonX.ReadValue<float>();   
                } else if(_inputAssetObj.roboticArm.enabled){
                    Xbox_X_KeyPressed_Float =  _inputAssetObj.roboticArm.ButtonX.ReadValue<float>();   
                }      
            }       
        }
        return Xbox_X_KeyPressed_Float;
    }  
    
    public static float Gamepad_DPAD_Up()
    {
        if(Input.GetAxis("DPAD_Vertical")> 0)// [Axis 7]
        {
            return 1.0f;
        } else { // <= 0
            return 0.0f;
        }
    }
    
    public static float Gamepad_DPAD_Down()
    {
        if(Input.GetAxis("DPAD_Vertical")< 0)// [Axis 7]
        {
            return 1.0f;
        } else { // >= 0
            return 0.0f;
        }
    } 

    // Specfic action functions
    ///////////////////////////

    public static bool PauseGame()
    {
        if (InputManager.Gamepad_ButtonView() || Input.GetKeyDown(KeyCode.P))
        {
            return true;
        } else {
            return false;
        }
    }


    public static bool ReturnToMainMenu()
    {
        if (InputManager.Gamepad_ButtonMenu() ||
            Input.GetKeyDown(KeyCode.Escape) ||
            Input.GetKeyDown(KeyCode.Delete))
        {
            return true;
        } else {
            return false;
        }
    }
    
    public static bool toggleChooseDifficulty(SpacecraftInput _inputAssetObj)
    {
           // Start game (Y button)
        
        bool _togglePanel = false;
        if(InputManager.useOldInputAndGamepad("InputManager.cs: toggleChooseDifficulty"))
        {
            if(InputManager.Gamepad_ButtonY())_togglePanel = true;
        }else {
            if(_inputAssetObj.menuScreen.competitionMission.ReadValue<float>() == 0)S_toggleChooseDifficultyHeld = true;
        
            if(_inputAssetObj.menuScreen.competitionMission.ReadValue<float>() > 0 && S_toggleChooseDifficultyHeld)
            {
                //if(S_VERBOSE)Debug.Log(" TOGGLE GAMEPADCONTROLS SCREEN");
                S_toggleChooseDifficultyHeld = false;

                _togglePanel = true;            
            }
        } 
        return _togglePanel;
    }

    public static bool toggleHUD(SpacecraftInput _inputAssetObj, bool _currentState)
    {
        bool _newState = _currentState;
        
        // toggle controllers on HUD
        float controllerHUD_Float = _inputAssetObj.flySpacecraft.ToggleHUDController.ReadValue<float>();
        if(controllerHUD_Float==0) S_controllerHUD_alreadyPushed = false;
        
        if(controllerHUD_Float>0 && !S_controllerHUD_alreadyPushed)
        {
            //if(S_VERBOSE)Debug.Log( "toggleHUD:"+ controllerHUD_Float);
            
            S_controllerHUD_alreadyPushed = true;
            if(_currentState)
            {
                _newState= false;
            } else {
                _newState = true;
            }
        }
        return _newState;
    }


    public static bool toggleDiagnosticWindow(SpacecraftInput _inputAssetObj, bool _currentState)
    {
        bool _newState = _currentState;
        // toggle diagnostic window
        float diagnosticWindow_Float = _inputAssetObj.flySpacecraft.DiagnosticToggle.ReadValue<float>();
        
        if(diagnosticWindow_Float==0) S_diagnosticWindowAlreadyPushed = false;
        
        if(diagnosticWindow_Float>0 && !S_diagnosticWindowAlreadyPushed)
        {
            //if(S_VERBOSE)Debug.Log( "toggleDiagnosticWindow:"+ diagnosticWindow_Float);
            
            
            S_diagnosticWindowAlreadyPushed = true;
            if(_currentState)
            {
                _newState= false;
            } else {
                _newState = true;
            }
        }
        return _newState;
    }

    public static bool CheatModeActive(SpacecraftInput _inputAssetObj)
    {
        bool _CheatModeActive = false; 
        
        if(InputManager.useOldInputAndGamepad("InputManager.cs : CheatModeActive()"))
        {
            if(InputManager.Gamepad_ButtonLS() && 
               InputManager.Gamepad_ButtonRS() &&  
               InputManager.Gamepad_ButtonA())
            {
                _CheatModeActive = true;
            }
        } else {
            float leftShoulder_Float   = _inputAssetObj.flySpacecraft.LeftTrigger.ReadValue<float>();
            float rightShoulder_Float   = _inputAssetObj.flySpacecraft.RightTrigger.ReadValue<float>();
            if(leftShoulder_Float   > 0 && rightShoulder_Float>0)
            {
                if(_inputAssetObj.flySpacecraft.ButtonA.ReadValue<float>() > 0)
                {
                    _CheatModeActive = true;
                }
            }
        }
        return _CheatModeActive;
    }

    public static bool checkIfActionMapEnabled(SpacecraftInput _inputAssetObj, 
                                               int _spacecraftControls,
                                               bool _leftHandedControls)
    {   
        bool actionMapEnabled = false;

        if(_leftHandedControls)
        {
            if(_spacecraftControls == Constants.SpacecraftControl)
            {
                if(_inputAssetObj.flySpacecraft_LH.enabled)
                {
                    actionMapEnabled = true;
                }
            } else if(_spacecraftControls == Constants.RoboticArmControl){ // Robot arm controls
                if(_inputAssetObj.roboticArm_LH.enabled)
                {
                    actionMapEnabled = true;
                }
            } else {
                Debug.LogError("InputManager.cs: checkIfactionMapEnabled(): Unknown control method [LH]");
            }         
        } else {
            if(_spacecraftControls == Constants.SpacecraftControl)
            {
                if(_inputAssetObj.flySpacecraft.enabled)
                {
                    actionMapEnabled = true;
                }
            } else if(_spacecraftControls == Constants.RoboticArmControl){ // Robot arm controls
                if(_inputAssetObj.roboticArm.enabled)
                {
                    actionMapEnabled = true;
                }
            } else {
                Debug.LogError("InputManager.cs: checkIfactionMapEnabled(): Unknown control method");
            }  
        }
        return actionMapEnabled;
    }

    public static bool checkIfActionMapDisabled(SpacecraftInput _inputAssetObj, 
                                               int _spacecraftControls,
                                               bool _leftHandedControls)
    {
        if(checkIfActionMapEnabled(_inputAssetObj, 
                                   _spacecraftControls,
                                   _leftHandedControls))
        {
            return false;
        } else {
            return true;
        }
    }


// FLOATS
    
    public static float rotateOrRoll(SpacecraftInput _inputAssetObj,
                                   int _rotationDirection,
                                   int _spacecraftControls,
                                   bool _leftHandedControls)
    {
        float rotateOrRoll_Float = 0.0f;
        if(InputManager.useOldInputAndGamepad("spacecraftController.cs : update controls()"))
        {
            if(_rotationDirection == Constants.Left)
            {
                rotateOrRoll_Float = InputManager.Gamepad_ButtonLS_Hold()? 1.0f : 0.0f;
            } else  if(_rotationDirection == Constants.Right){
                rotateOrRoll_Float = InputManager.Gamepad_ButtonRS_Hold()? 1.0f : 0.0f;
            }
        } else {
            if(_leftHandedControls)
            {
                if(_spacecraftControls == Constants.SpacecraftControl)
                {
                    if(_rotationDirection == Constants.Left)
                    {
                        rotateOrRoll_Float = _inputAssetObj.flySpacecraft_LH.rotateLeft.ReadValue<float>();
                    } else if(_rotationDirection == Constants.Right){
                        rotateOrRoll_Float = _inputAssetObj.flySpacecraft_LH.rotateRight.ReadValue<float>();
                    }
                }else if(_spacecraftControls == Constants.RoboticArmControl){ // Robot arm controls
                  if(_rotationDirection == Constants.Left)
                    {
                        rotateOrRoll_Float = _inputAssetObj.roboticArm_LH.rotateLeft.ReadValue<float>();
                    } else if(_rotationDirection == Constants.Right){
                        rotateOrRoll_Float = _inputAssetObj.roboticArm_LH.rotateRight.ReadValue<float>();
                    }
                } else {
                    if(S_VERBOSE)Debug.LogError("InputManager.cs: rotateOrRoll(): Unknown control method");
                }
            } else {
                if(_spacecraftControls == Constants.SpacecraftControl)
                {
                    if(_rotationDirection == Constants.Left)
                    {
                        rotateOrRoll_Float = _inputAssetObj.flySpacecraft.rotateLeft.ReadValue<float>();
                    } else if(_rotationDirection == Constants.Right){
                        rotateOrRoll_Float = _inputAssetObj.flySpacecraft.rotateRight.ReadValue<float>();
                    }
                }else if(_spacecraftControls == Constants.RoboticArmControl){ // Robot arm controls
                  if(_rotationDirection == Constants.Left)
                    {
                        rotateOrRoll_Float = _inputAssetObj.roboticArm.rotateLeft.ReadValue<float>();
                    } else if(_rotationDirection == Constants.Right){
                        rotateOrRoll_Float = _inputAssetObj.roboticArm.rotateRight.ReadValue<float>();
                    }
                } else {
                    if(S_VERBOSE)Debug.LogError("InputManager.cs: rotateOrRoll(): Unknown control method");
                }
            }
        }

        return rotateOrRoll_Float;
    }

    public static float resetArm(SpacecraftInput _inputAssetObj,
                                   bool _leftHandedControls)
    {
         float resetArm_Float = 0.0f;

        if(InputManager.useOldInputAndGamepad("spacecraftController.cs : resetArm()"))
        {
            InputManager.Gamepad_ButtonRTS();
        } else {
            if(_leftHandedControls)
            {
                resetArm_Float= _inputAssetObj.roboticArm_LH.resetArm.ReadValue<float>();
            } else {
                resetArm_Float= _inputAssetObj.roboticArm.resetArm.ReadValue<float>();
            }
        }
       return resetArm_Float; 
    }


// VECTORS


    public static Vector2 pitchYawVector(SpacecraftInput _inputAssetObj, 
                                         int _controllerMethod,
                                         bool _leftHandedControls, 
                                         bool _invertVertical)
    {
        
        Vector2 _pitchYaw_Vector = new Vector2(0.0f, 0.0f);

        if(_controllerMethod == Constants.KeyboardAndMouse) // Keyboard with mouse for direction
        {
            if(_leftHandedControls)
            {
                _pitchYaw_Vector  = _inputAssetObj.flySpacecraft_LH.MouseMovement.ReadValue<Vector2>();
            } else {
                _pitchYaw_Vector  = _inputAssetObj.flySpacecraft.MouseMovement.ReadValue<Vector2>();
            }
            
        } else //Keyboard only or gamepad
        {
            // If we're using the game pad and the old input manager...
            if(InputManager.useOldInputAndGamepad("InputManager.cs : pitchYawVector()"))
            {
                _pitchYaw_Vector.y = -Input.GetAxis("RightThumbStick_Vertical"); // [Axis 5]
                _pitchYaw_Vector.x = Input.GetAxis("RightThumbStick_Horizontal");   // [Axis 4]
            } else {
                if( _leftHandedControls)
                {
                    _pitchYaw_Vector = _inputAssetObj.flySpacecraft_LH.AttitudePitchYaw.ReadValue<Vector2>();
                } else {
                    _pitchYaw_Vector = _inputAssetObj.flySpacecraft.AttitudePitchYaw.ReadValue<Vector2>();
                 }
            }
        }
        if(_invertVertical)
        {
            _pitchYaw_Vector.y *= -1.0f;
        }
        return _pitchYaw_Vector;
    }
    
    
    public static float moveYVector(SpacecraftInput _inputAssetObj, 
                                         int _controllerMethod,
                                         bool _leftHandedControls, 
                                         float _mouseSpeedModifier)
    {
        
        float _moveYVector = 0.0f;
        Vector2 _deltaVec = new Vector2(0.0f, 0.0f);
        float speedModifier = _mouseSpeedModifier; // when using mouse, scales input to get correct motion of robotic arm.
        
        if(_controllerMethod == Constants.KeyboardAndMouse) // Keyboard with mouse for direction
        {
            if(_leftHandedControls)
            {
                _deltaVec  =  _inputAssetObj.roboticArm_LH.MouseMovement.ReadValue<Vector2>();
            } else {
                _deltaVec  =  _inputAssetObj.roboticArm.MouseMovement.ReadValue<Vector2>();    
            }

            if(_deltaVec.y >0)_moveYVector = 1.0f;
            if(_deltaVec.y <0)_moveYVector = -1.0f;
            
        } else {//Keyboard only or gamepad
            
            if(InputManager.useOldInputAndGamepad("InputManager.cs : moveYVector()"))
            {
                _moveYVector = Input.GetAxis("LeftThumbStick_Vertical");   // [Axis Y]
            } else {
            
                if(_leftHandedControls)
                {
                    _deltaVec  =  _inputAssetObj.roboticArm_LH.rightStick.ReadValue<Vector2>();
                } else {
                    _deltaVec  =  _inputAssetObj.roboticArm.rightStick.ReadValue<Vector2>();    
                }
                _moveYVector = _deltaVec.y;
            }
        }
        return _moveYVector;
    }

    public static Vector2 moveYXPlane(SpacecraftInput _inputAssetObj, 
                                         int _controllerMethod,
                                         bool _leftHandedControls, 
                                         float _mouseSpeedModifier)
    {
        
       Vector2  _moveYXVector = new Vector2(0.0f, 0.0f);
        Vector2 _deltaVec = new Vector2(0.0f, 0.0f);
        float speedModifier = _mouseSpeedModifier; // when using mouse, scales input to get correct motion of robotic arm.
        
        if(_controllerMethod == Constants.KeyboardAndMouse) // Keyboard with mouse for direction
        {
            if(_leftHandedControls)
            {
                _deltaVec  =  _inputAssetObj.roboticArm_LH.MouseMovement.ReadValue<Vector2>();
            } else {
                _deltaVec  =  _inputAssetObj.roboticArm.MouseMovement.ReadValue<Vector2>();    
            }

            if(_deltaVec.y >0)_moveYXVector.y = 1.0f;
            if(_deltaVec.y <0)_moveYXVector.y = -1.0f;
            if(_deltaVec.x >0)_moveYXVector.x = 1.0f;
            if(_deltaVec.x <0)_moveYXVector.x = -1.0f;

        } else {//Keyboard only or gamepad
            
            if(InputManager.useOldInputAndGamepad("InputManager.cs : moveYVector()"))
            {
                _moveYXVector.y = -Input.GetAxis("RightThumbStick_Vertical");   // [Axis 5]
                _moveYXVector.x = Input.GetAxis("RightThumbStick_Horizontal");   // [Axis 4]
            } else {
            
                if(_leftHandedControls)
                {
                    _moveYXVector =  _inputAssetObj.roboticArm_LH.rightStick.ReadValue<Vector2>();
                } else {
                    _moveYXVector =  _inputAssetObj.roboticArm.rightStick.ReadValue<Vector2>();    
                }
            }
        }
        return _moveYXVector;
    }


    // Get input from left stick (on gamepad) or keyboard input
    public static Vector2 moveXZVector(SpacecraftInput _inputAssetObj, 
                                         int _controllerMethod,
                                         bool _leftHandedControls)
    {
        Vector2 _moveXZ_Vector = new Vector2(0.0f, 0.0f);
        
        if(InputManager.useOldInputAndGamepad("InputManager.cs : moveXZVector()"))
        {
            _moveXZ_Vector.x = Input.GetAxis("LeftThumbStick_Horizontal"); // [Axis X]
            _moveXZ_Vector.y = -Input.GetAxis("LeftThumbStick_Vertical");   // [Axis Y]
        } else {
            if( _leftHandedControls)
            {
                if(_inputAssetObj.roboticArm_LH.enabled)
                {
                    _moveXZ_Vector = _inputAssetObj.roboticArm_LH.leftStick.ReadValue<Vector2>();
                } else if(_inputAssetObj.flySpacecraft_LH.enabled){
                    _moveXZ_Vector = _inputAssetObj.flySpacecraft_LH.MoveXZ.ReadValue<Vector2>();
                } else {
                    Debug.LogError(" Error - no allowable action map defined in moveXZVector (LH)");
                }  
            } else {
                if(_inputAssetObj.roboticArm.enabled)
                {
                    _moveXZ_Vector = _inputAssetObj.roboticArm.leftStick.ReadValue<Vector2>();
                } else if(_inputAssetObj.flySpacecraft.enabled){
                    _moveXZ_Vector = _inputAssetObj.flySpacecraft.MoveXZ.ReadValue<Vector2>();
                } else {
                    Debug.LogError(" Error - no allowable action map defined in moveXZVector");
                }
             }
        }
        return _moveXZ_Vector;
    }

    
    
    // Get input from left stick (on gamepad) or keyboard input, y direction only
    public static float moveZFloat(SpacecraftInput _inputAssetObj, 
                                         int _controllerMethod,
                                         bool _leftHandedControls)
    {

         Vector2 _moveXZ_Vector = moveXZVector(_inputAssetObj,_controllerMethod,_leftHandedControls);
         return _moveXZ_Vector.y;
    }

    // Logical to check whether spaceflight controls are active or not
    public static bool spaceflightControlsActive(SpacecraftInput _inputAssetObj, bool _leftHandedControls)
    {
        bool flySpacecraftEnabled = false;
        if(_leftHandedControls)
        {
            if(_inputAssetObj.flySpacecraft_LH.enabled)flySpacecraftEnabled= true;
        } else {
            if(_inputAssetObj.flySpacecraft.enabled)flySpacecraftEnabled= true; 
        }
        if(S_VERBOSE)Debug.Log(" spaceflightControlsActive: " + flySpacecraftEnabled);
        return flySpacecraftEnabled;
    }

    // Logical to check whether spaceflight controls are active or not
    public static bool robotControlsActive(SpacecraftInput _inputAssetObj, bool _leftHandedControls)
    {
        bool robotControlsEnabled = false;
        if(_leftHandedControls)
        {
            if(_inputAssetObj.roboticArm_LH.enabled)robotControlsEnabled= true;
        } else {
            if(_inputAssetObj.roboticArm.enabled)robotControlsEnabled= true; 
        }
        if(S_VERBOSE)Debug.Log(" spaceflightControlsActive: " + robotControlsEnabled);
        return robotControlsEnabled;
    }

    public static void toggleSpacecraftRobotArm(SpacecraftInput _inputAssetObj, bool _leftHandedControls)
    {
        if(InputManager.useOldInputAndGamepad("InputManager.cs : toggleSpacecraftRobotArm()"))
        {
            if(InputManager.Gamepad_ButtonY())
            {
                // Left hand/right hand doesn't matter on gamepad
                if(_inputAssetObj.flySpacecraft.enabled)
                {
                    InputManager.enableActionMap(_inputAssetObj, _inputAssetObj.roboticArm);
                    if(S_VERBOSE)Debug.Log(" Switch to robotic arm: spacecraft enabled:"+ _inputAssetObj.flySpacecraft.enabled);
                } else if(_inputAssetObj.roboticArm.enabled){
                    InputManager.enableActionMap(_inputAssetObj, _inputAssetObj.flySpacecraft);
                    if(S_VERBOSE)Debug.Log(" Switch to spacecraft: spacecraft enabled:"+ _inputAssetObj.flySpacecraft.enabled);
                }
 
            }
        } else {

            // Check if using left-handed controls (for keyboard)
            if(_leftHandedControls)
            {
                if(_inputAssetObj.flySpacecraft_LH.enabled)
                {
                    if(_inputAssetObj.flySpacecraft_LH.SwitchToRobotControl.ReadValue<float>() > 0)
                    {
                        InputManager.enableActionMap(_inputAssetObj, _inputAssetObj.roboticArm_LH);
                    }
                } else if(_inputAssetObj.roboticArm_LH.enabled){
                    if(_inputAssetObj.roboticArm_LH.SwitchToSpacecraftControl.ReadValue<float>() > 0)
                    {
                        InputManager.enableActionMap(_inputAssetObj, _inputAssetObj.flySpacecraft_LH);
                    }
                }
            } else {
                if(_inputAssetObj.flySpacecraft.enabled)
                {
                    if(_inputAssetObj.flySpacecraft.SwitchToRobotControl.ReadValue<float>() > 0)
                    {
                        InputManager.enableActionMap(_inputAssetObj, _inputAssetObj.roboticArm);
                    }
                } else if(_inputAssetObj.roboticArm.enabled){
                    if(_inputAssetObj.roboticArm.SwitchToSpacecraftControl.ReadValue<float>() > 0)
                    {
                        InputManager.enableActionMap(_inputAssetObj, _inputAssetObj.flySpacecraft);
                    }
                }
            }
        }
        return;
    }


    public static bool cameraZoom(SpacecraftInput _inputAssetObj)
    {
        bool _zoom =false;
        
        if(InputManager.useOldInputAndGamepad("InputManager.cs : cameraZoom()"))
        {
            if(Input.GetButton("LT_button")) _zoom =true;
        } else {
            if(_inputAssetObj.flySpacecraft.Zoom.ReadValue<float>()>0 ||
               _inputAssetObj.flySpacecraft_LH.Zoom.ReadValue<float>()>0 ||
               Input.GetMouseButton(2))
            {
                _zoom =true;
            }
        }
        return  _zoom;
    }

    
    public static float zeroRelVel(SpacecraftInput _inputAssetObj, bool _leftHandedControls)
    {
        float zeroRelVel_Float = 0.0f;

        if(InputManager.useOldInputAndGamepad("InputManager.cs : zeroRelVel()"))
        {
            zeroRelVel_Float = InputManager.Gamepad_ButtonRTS() ? 1.0f : 0.0f; // Press right-stick
        } else {
            if(_leftHandedControls)
            {
                zeroRelVel_Float = _inputAssetObj.flySpacecraft_LH.ZeroRelVel.ReadValue<float>();
            } else {
                zeroRelVel_Float = _inputAssetObj.flySpacecraft.ZeroRelVel.ReadValue<float>();
            }
        }
        return zeroRelVel_Float;
    }
    
    public static float thrustUp(SpacecraftInput _inputAssetObj, bool _leftHandedControls)
    {
        float thrustUp_Float = 0.0f;

        if(InputManager.useOldInputAndGamepad("InputManager.cs : thrustUp()"))
        {
            thrustUp_Float = InputManager.Gamepad_DPAD_Up(); // Up D-pad
        } else {
            if(_leftHandedControls)
            {
                thrustUp_Float = _inputAssetObj.flySpacecraft_LH.ThrustUp.ReadValue<float>();
            } else {
                thrustUp_Float = _inputAssetObj.flySpacecraft.ThrustUp.ReadValue<float>();
            }
        }
        return thrustUp_Float;
    }
    
    public static float thrustDown(SpacecraftInput _inputAssetObj, bool _leftHandedControls)
    {
        float thrustDown_Float = 0.0f;

        if(InputManager.useOldInputAndGamepad("InputManager.cs : thrustDown()"))
        {
            thrustDown_Float = InputManager.Gamepad_DPAD_Down(); // Down D-pad
        } else {
            if(_leftHandedControls)
            {
                thrustDown_Float = _inputAssetObj.flySpacecraft_LH.ThrustDown.ReadValue<float>();
            } else {
                thrustDown_Float = _inputAssetObj.flySpacecraft.ThrustDown.ReadValue<float>();
            }
        }
        return thrustDown_Float;
    }

    public static float gripper_Float(SpacecraftInput _inputAssetObj, bool _leftHandedControls)
    {
        // 6) open/close gripper
        float closeGrip_Float = 0.0f;
        float openGrip_Float  = 0.0f;
        float grip_Float = 0.0f;
        if(InputManager.useOldInputAndGamepad("InputManager.cs : gripper_Float()"))
        {
            grip_Float  = -InputManager.Gamepad_Triggers();// use minus sign so left trigger opens grip, not closes
        }else {
            if(_leftHandedControls)
            {

                // First check which action map we're using, as we can now use the gripper directly when flying the spacecraft
                if(_inputAssetObj.roboticArm_LH.enabled)
                {
                    closeGrip_Float  =  _inputAssetObj.roboticArm_LH.leftTrigger.ReadValue<float>();
                    openGrip_Float   =  _inputAssetObj.roboticArm_LH.rightTrigger.ReadValue<float>();
                } else if(_inputAssetObj.flySpacecraft_LH.enabled){
                    closeGrip_Float  =  _inputAssetObj.flySpacecraft_LH.LeftTrigger.ReadValue<float>();
                    openGrip_Float   =  _inputAssetObj.flySpacecraft_LH.RightTrigger.ReadValue<float>();
                } else if(_inputAssetObj.enterScoreScreen.enabled){
                 // Mission completed!
                }else {
                    Debug.LogError(" Error - no allowable action map defined in gripper control");
                }

            }else{

                if(_inputAssetObj.roboticArm.enabled)
                {
                    closeGrip_Float  =  _inputAssetObj.roboticArm.leftTrigger.ReadValue<float>();
                    openGrip_Float   =  _inputAssetObj.roboticArm.rightTrigger.ReadValue<float>();
                } else if(_inputAssetObj.flySpacecraft.enabled){
                closeGrip_Float  =  _inputAssetObj.flySpacecraft.LeftTrigger.ReadValue<float>();
                openGrip_Float   =  _inputAssetObj.flySpacecraft.RightTrigger.ReadValue<float>();
                } else if(_inputAssetObj.enterScoreScreen.enabled){
                 // Mission completed!
                }else {
                    Debug.LogError(" Error - no allowable action map defined in gripper control");
                }
            }  
            grip_Float = openGrip_Float - closeGrip_Float; //if >0, open, if <0 close
        }
        return grip_Float;
    }

    public static bool EnterScoreScreen(SpacecraftInput _inputAssetObj)
    {
        bool EnterScoreScreen = false;
        float EnterScoreScreen_Float = 0.0f;
        if(InputManager.useOldInputAndGamepad("InputManager.cs: EnterScoreScreen"))
        {
            if(InputManager.Gamepad_ButtonX())EnterScoreScreen = true;
        } else {
            EnterScoreScreen_Float =  _inputAssetObj.enterScoreScreen.ButtonX.ReadValue<float>();
            if(EnterScoreScreen_Float > 0.0f)EnterScoreScreen = true;
        }
        // Returns true if enter key is used
        if (Input.GetKeyDown(KeyCode.Return))EnterScoreScreen = true;
        
        return EnterScoreScreen;
    }

    public static void EnableCameraManager(SpacecraftInput _inputAssetObj)
    {
        if(InputManager.useOldInputAndGamepad("InputManager.cs: EnableCameraManager"))
        {
            
        } else {
            _inputAssetObj.flySpacecraft_LH.toggleCamera.Enable();
            _inputAssetObj.flySpacecraft.toggleCamera.Enable();  
            _inputAssetObj.roboticArm_LH.toggleCamera.Enable();
            _inputAssetObj.roboticArm.toggleCamera.Enable();
        }
    }

    public static bool ToggleCameras(SpacecraftInput _inputAssetObj, bool _leftHandedControls)
    {
        bool toggleCamera = false;
        float toggleCamera_Float = 0.0f;

        if(InputManager.useOldInputAndGamepad("InputManager.cs: ToggleCamera"))
        {
            if(InputManager.Gamepad_ButtonX())toggleCamera =true;

        } else {
           
            if(_leftHandedControls)
            {
                if(_inputAssetObj.flySpacecraft_LH.enabled)
                {
                    toggleCamera_Float = _inputAssetObj.flySpacecraft_LH.toggleCamera.ReadValue<float>();
                } else if (_inputAssetObj.roboticArm_LH.enabled)
                {
                    toggleCamera_Float = _inputAssetObj.roboticArm_LH.toggleCamera.ReadValue<float>();
                } 
            } else {
                if(_inputAssetObj.flySpacecraft.enabled)
                {
                    toggleCamera_Float = _inputAssetObj.flySpacecraft.toggleCamera.ReadValue<float>();
                } else if (_inputAssetObj.roboticArm.enabled)
                {
                    toggleCamera_Float = _inputAssetObj.roboticArm.toggleCamera.ReadValue<float>();
                }    
            }
            if(toggleCamera_Float == 0)S_cameraToggle_alreadyPushed = false;
            
            if(toggleCamera_Float>0 && !S_cameraToggle_alreadyPushed)
            {
                toggleCamera= true;
                S_cameraToggle_alreadyPushed = true;
            }
        }
        return toggleCamera;
    }
    */

}
