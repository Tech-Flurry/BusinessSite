// Import the functions you need from the SDKs you need
import { initializeApp } from "https://www.gstatic.com/firebasejs/9.9.3/firebase-app.js";
import { getAnalytics } from "https://www.gstatic.com/firebasejs/9.9.3/firebase-analytics.js";
import { getPerformance } from "https://www.gstatic.com/firebasejs/9.9.3/firebase-performance.js";
import { getMessaging } from "https://www.gstatic.com/firebasejs/9.9.3/firebase-messaging.js";
import { getStorage } from "https://www.gstatic.com/firebasejs/9.9.3/firebase-storage.js";
import { ref } from "https://www.gstatic.com/firebasejs/9.9.3/firebase-storage.js";
import { uploadString } from "https://www.gstatic.com/firebasejs/9.9.3/firebase-storage.js";
import { getDownloadURL } from "https://www.gstatic.com/firebasejs/9.9.3/firebase-storage.js";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries
// https://firebase.google.com/docs/web/learn-more#libraries-cdn

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
    apiKey: "AIzaSyANDGk78M_A7S9vlHrFOCRy4G3GJ-xkeG4",
    authDomain: "techflurry-6f21d.firebaseapp.com",
    projectId: "techflurry-6f21d",
    storageBucket: "techflurry-6f21d.appspot.com",
    messagingSenderId: "401074944157",
    appId: "1:401074944157:web:8c8b8bc159766c79d6fc74",
    measurementId: "G-NCNKERV8WN"
};

// Initialize Firebase
var app;
var analytics;
var perf;
var messaging;
var storage;
var _dotnetReference;


export function initFirebase (dotnetReference) {
    _dotnetReference = dotnetReference;
    if (!app) {
        app = initializeApp(firebaseConfig);
    }
    if (!analytics) {
        analytics = getAnalytics(app);
    }
    if (!perf) {
        perf = getPerformance(app);
    }
    if (!messaging) {
        messaging = getMessaging(app);
    }
    if (!storage) {
        storage = getStorage(app);
    }
}

export function uploadImage (filename, base64, contentType, completedCallback) {
    const storageRef = ref(storage, 'blogs-images/' + filename);
    const metadata = {
        contentType: contentType,
    };
    uploadString(storageRef, base64, 'base64', metadata).then((snapshot) => {
        if (completedCallback) {
            _dotnetReference.invokeMethodAsync(completedCallback);
        }
    });
}

export function populateImage (img, filename, successCallback, errorCallback) {
    getDownloadURL(ref(storage, 'blogs-images/' + filename))
        .then((url) => {
            console.log(url);
            $(img).attr("src", url);
            //const img = document.getElementById('myimg');
            //img.setAttribute('src', url);

            if (successCallback) {
                _dotnetReference.invokeMethodAsync(successCallback, filename);
            }
        })
        .catch((error) => {
            // Handle any errors
            if (errorCallback) {
                _dotnetReference.invokeMethodAsync(errorCallback, filename);
            }
        });
}