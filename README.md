<h1 align="center">🚀 Echo Runner</h1>
<p align="center">
  A rhythm-based sci-fi runner built with Unity.<br>
  <b>Dodge. Jump. Crouch. To the Beat.</b>
</p>

<p align="center">
  <img src="https://github.com/Aaravjaii/ECHO-RUNNERS/blob/master/image/Screenshot%202025-07-22%20123250.png?raw=true" width="80%" alt="Gameplay Screenshot 1"><br><br>
  <img src="https://github.com/Aaravjaii/ECHO-RUNNERS/blob/master/image/Screenshot%202025-07-22%20123258.png?raw=true" width="80%" alt="Gameplay Screenshot 2"><br><br>
  <img src="https://github.com/Aaravjaii/ECHO-RUNNERS/blob/master/image/Screenshot%202025-07-22%20123311.png?raw=true" width="80%" alt="Gameplay Screenshot 3">
</p>

---

## 🎮 Gameplay Overview

Echo Runner is a rhythm-based endless runner where you dodge obstacles on beat to earn high scores and keep your combo streak alive. 

| Action         | Key             |
|----------------|------------------|
| ⬅️ / ➡️        | Switch Lanes     |
| ⬆️ (Jump)      | Spacebar         |
| ⬇️ (Crouch)    | Left Ctrl        |

- 🟢 Perfect → +20  
- 🟡 Good → +10  
- 🔴 Miss → Combo Break + Screen Shake

---

## ✨ Features

- 🔊 Beat-synced obstacle spawning (powered by BPM)
- 🌌 Sci-fi visuals with asteroid terrain & dynamic skybox
- 🛣️ 3-lane runner mechanics with jump and crouch
- 🧠 Smart obstacle cooldown system + difficulty scaling
- 📈 Score + Combo System + Hit Rating Text
- 🎇 Particle FX + Camera Shake on "Miss"
- 🎧 Custom BGM & Game Over audio via manager scripts
- 🎞️ Fade-in/fade-out animation for obstacles
- 🕹️ Main Menu → Loading → Game → Game Over flow
- ⏳ Custom loading screen with animated wait (10s min)

---

## 📂 Project Structure

```text
Assets/
├── Scripts/
│   ├── PlayerMovement.cs
│   ├── BeatManager.cs
│   ├── ObstacleSpawner.cs
│   └── GameOverManager.cs
├── Scenes/
│   ├── MainMenu.unity
│   ├── Loading.unity
│   └── SampleScene.unity
├── Audio/
│   ├── BackgroundMusic.mp3
│   └── GameOver.wav
├── Prefabs/
│   └── Obstacles (Laser, Drone, Rock)
├── UI Elements/
