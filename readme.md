Welcome to my collection of scripts to use in Unity!

This are utils that I use in almost every project, and has a very general use case. If it's here, it's useful and will save you some time when programming your game!

## Yield Instructions
### [DoEveryFrameFor.cs](/Scripts/DoEveryFrameFor.cs)

This script allows you to run a loop function inside an IEnumerator, to make things like a fade in.

In this example, the code inside the action will run every frame for 2 seconds, returning the completion value. In first frame, value will be 0, and the last frame, when reached the 2 seconds, will be 1. This will result in a linear progressive fade in to the volume of the source.

```c#
// Fade in example using DoEveryFrameFor
public IEnumerator FadeIn(){

  yield return new DoEveryFrameFor(2f, (t)=>{
    someAudioSource.volume = t;
  });
  
}
```

That is equivalent to this (but with more settings like a delay and a boolean to use unscaled delta time instead):
```c#

// Manual fade in example
public IEnumerator ManualFadeIn(){
  
  float currentTime = 0f;
  float loopTime = 2f;

  while( currentTime < loopTime ){
    someAudioSource.volume = currentTime / loopTime;
    
    currentTime += Time.deltaTime;
    yield return new WaitForEndOfFrame();
  }

}
```
