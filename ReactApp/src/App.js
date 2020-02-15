import React from "react";
import "./App.css";
import Events from "./components/Events";

function App() {
  const API_INVOKE_URL = "https://a01089042ticketapi.azurewebsites.net/api/";
  return (
    <div className="App">
      <h1>KORE STADIUM</h1>
      <h3>Events</h3>
      <Events API_URL={API_INVOKE_URL} />
    </div>
  );
}

export default App;
