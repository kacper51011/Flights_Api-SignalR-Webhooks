import { useState } from "react";
import "./App.css";
import { FlightList } from "./Components/FlightList";

function App() {
  const [count, setCount] = useState(0);

  return (
    <div className="w-full h-screen flex flex-col items-center bg-gray-900">
      <h1 className="font-bold text-3xl text-yellow-500 font-serif">Current Flights</h1>
      <FlightList />
    </div>
  );
}

export default App;
