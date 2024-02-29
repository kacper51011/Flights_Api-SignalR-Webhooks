import { useState } from "react";
import "./App.css";
import { HubConnectionBuilder } from "@microsoft/signalr";
import { ItemLetter } from "./Components/ItemLetter";

function App() {
  // const [connection, setConnection] = useState(null);

  // const connectToServer = async () => {
  //   try {
  //     const conn = new HubConnectionBuilder().withUrl("https://localhost:8005").build();

  //     conn.on("InitializedConnection", (flights: string) => {});
  //   } catch (error) {}
  // };

  const [letter, setLetter] = useState("A");

  return (
    <div className="w-full h-screen flex flex-col items-center bg-gray-900">
      <h1 className="font-bold text-3xl text-yellow-500 font-serif m-4">Current Flights</h1>

      {/* <FlightList /> */}
      <ItemLetter letter={letter} />
      <button
        onClick={() => {
          setLetter("A");
        }}
      >
        kliknij A
      </button>

      <button
        onClick={() => {
          setLetter("B");
        }}
      >
        kliknij B
      </button>
    </div>
  );
}

export default App;
