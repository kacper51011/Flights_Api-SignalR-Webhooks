import { useState } from "react";

import "./App.css";
import { ItemLetter } from "./Components/ItemLetter";

function App() {
  const [count, setCount] = useState(0);

  return (
    <div className="w-full flex flex-col items-center">
      <h1 className="font-bold text-3xl text-pretty font-serif">Current Flights</h1>
      <ItemLetter letter="H" />
    </div>
  );
}

export default App;
