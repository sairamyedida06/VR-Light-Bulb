# VR Light Bulb – Interaction Assessment

This is my submission for the VR interaction assessment. The task was to build a small VR interaction where the user grabs a bulb, places it into a socket, turns on the power, and controls the brightness with a slider.

I hadn't worked with VR before this, so I learned the XR Interaction Toolkit (XRI) for this assessment. I went through Unity's XR docs and Learn material to understand the components, and built everything step by step. I don't have a headset, so I tested the whole thing using the XR Device Simulator (keyboard + mouse) in the editor.

## Unity / Package info
- Unity 6 (6000.3 LTS)
- Render pipeline: URP
- XR Interaction Toolkit: 3.5.1
- Tested with: XR Device Simulator (no headset needed)

## How to open and test
1. Open the project in Unity 6.
2. Open the scene: `Assets/Scenes/BulbAssessment/BulbAssessment.unity`
3. Press Play.
4. Simulator controls (mouse + keyboard):
   - Right mouse button held = look around
   - Hold Left Shift (or press T) to control a controller
   - Left click = grip / select
5. Follow the on-screen tutorial prompts (grab bulb → put in socket → power on → move slider).

## Expected flow
1. Grab the bulb with the controller.
2. Place it into the socket – it snaps into position and stays seated. The socket only accepts the bulb (interaction layer filter).
3. Turn on the power switch – the switch animates and the bulb powers on.
4. Move the slider – it slides on one axis between min and max and controls the bulb brightness.
5. When power is on, the slider changes the brightness. When power is off, the bulb does not light no matter where the slider is.
6. Once the bulb is seated, power is on, and the slider has been used, a "Task Complete" message shows up.

## How it's structured (scripts)
I kept the logic split by responsibility instead of one big script.

- **PowerSwitch.cs** – holds the power state (on/off). Toggles when the switch is selected and drives the switch animation through an Animator bool. `IsPowerOn` is public get / private set so other scripts can read it but only the switch can change it.
- **SliderControl.cs** – turns the knob's position along the track into a 0–1 value that the bulb reads. Also tracks if the slider has been used (for the task complete check).
- **BulbLight.cs** – just controls the actual Light (set intensity / turn off). It only executes, it doesn't decide anything.
- **BulbController.cs** – the main controller. It reads the socket, the power switch and the slider, and decides the bulb's light. The bulb only lights when it's seated AND power is on. It also handles the "Task Complete" indication.
- **TutorialManager.cs** – shows a small prompt canvas near each item, one step at a time, and moves to the next step when that step is done.

## Some decisions I made
- I used **events** for the socket and grab (subscribe in OnEnable, unsubscribe in OnDisable) instead of checking every frame, so the code isn't polling and doesn't leave dangling listeners.
- The slider value is measured **relative to the track using the world position** (InverseTransformPoint), not localPosition. I did this because when XRI grabs the knob it re-parents it, and that was making localPosition break and the value jump to 0. Measuring against the track fixed it and works no matter what the knob is parented to.
- The bulb has a **minimum brightness floor** when powered on, so flipping the switch actually shows light even if the slider is at the low end. The slider still controls the brightness above that.
- I kept the switch, slider and socket each owning their own state, and let the controllers just read that state. I didn't split it into more scripts than needed – for a single bulb that would just add complexity.

## Notes
- Everything was tested in the XR Device Simulator since I don't have a headset.
