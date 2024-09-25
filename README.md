# GPS-AR-Android/iOS mobile app

## Table of Contents
- [Introduction](#introduction)
- [Features](#features)
- [Usage](#usage)
- [Benefits](#benefits)
- [Tools Used](#tools-used)
- [Requirements](#requirements)



Uploading Lightship ARDK.mp4…



## Introduction
The GPS AR App is a cross-platform application designed for Android and iOS that utilizes Augmented Reality (AR) to display real-time GPS data to users. By leveraging Firebase for real-time data synchronization, the app ensures that all users connected to the network see the same AR elements based on their geographical location. The app also features geofencing capabilities, allowing for location-based interactions, and supports touch interactions within the AR environment.

## Features
- **Real-Time GPS Synchronization:** Uses Firebase to synchronize GPS data across all users in real time.
- **Cross-Platform Compatibility:** Available on both Android and iOS.
- **AR Visualizations:** Display GPS-based data in an AR environment.
- **Geofencing:** Trigger specific AR events or notifications when users enter or exit predefined geographical areas.
- **Touch Interactions:** Supports interactive AR elements that users can manipulate via touch.

## Usage
1. **Setup and Installation:**
   - Build the .apk application for Android or sideload for the iOS device.
   - Connect a Firebase server or any other server for quick data storage and retrieval to initiate the app.

2. **Navigating the AR Environment:**
   - The app will request permission to access your location. Ensure that GPS is enabled on your device.
   - Once location permissions are granted, the app will load the Geo anchors in Augmented Reality  based on your geographical location.

3. **Interacting with AR Elements:**
   - Use touch gestures to interact with AR elements (e.g., tap, swipe).
   - As you move, AR objects will update based on real-time GPS data from Firebase.

4. **Geofencing:**
   - Set up geofences through the app's settings. 
   - When you enter or exit a geofenced area, AR events or notifications will be triggered.


## Benefits
- **Enhanced User Experience:** Offers an immersive AR experience with real-time data, making navigation and interaction more engaging.
- **Location-Based Services:** Geofencing adds value by providing context-aware content, useful for location-based marketing or personalized user experiences.
- **Cross-Platform Reach:** Designed for both Android and iOS, ensuring a wide user base.

## Tools Used
- **Unity:** For building the AR environment and cross-platform support.
- **AR Foundation:** To create the core logic behind the AR experiences.
- **ARCore and ARKit libraries:** To create AR experiences on Android and iOS respectively.
- **Firebase:** For real-time GPS data synchronization and user authentication.
- **Google Maps SDK:** For mapping and geolocation services.
- **Cloud Storage:** For location-based data storage and retrieval mechanisms.

## Requirements
- **Android 8.0 (Oreo) or higher**
- **iOS 11.0 or higher**
- **Unity 2022.3 or higher**
- **AR Foundation 5.0 or higher**
- **Firebase SDK**
- **Internet connection** for real-time data synchronization






