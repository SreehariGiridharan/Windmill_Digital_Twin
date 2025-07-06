// firebase-init.js

// Import Firebase modules
import { initializeApp } from "https://www.gstatic.com/firebasejs/10.3.1/firebase-app.js";
import { getDatabase, ref, onValue } from "https://www.gstatic.com/firebasejs/10.3.1/firebase-database.js";
import { set } from "https://www.gstatic.com/firebasejs/10.3.1/firebase-database.js";
// Your Firebase config
const firebaseConfig = {
  apiKey: "AIzaSyA5mIyvehlS8GZuMlvOu403VODOCEnQPtY",
  authDomain: "windmill-52347.firebaseapp.com",
  databaseURL: "https://windmill-52347-default-rtdb.firebaseio.com", // ✅ Realtime DB URL
  projectId: "windmill-52347",
  storageBucket: "windmill-52347.appspot.com",
  messagingSenderId: "420832522564",
  appId: "1:420832522564:web:18ea9a1cc2e1d0f4af6711",
  measurementId: "G-JN92JPBSVP"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const database = getDatabase(app);

// Make functions and references available globally
window.firebaseApp = app;
window.firebaseDatabase = database;
window.firebaseRef = ref;
window.firebaseOnValue = onValue;
// Make the `set` function available globally
window.firebaseSet = set;