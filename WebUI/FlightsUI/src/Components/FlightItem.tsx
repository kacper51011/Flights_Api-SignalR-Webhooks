import { useState } from "react";

export const FlightItem = () => {
  const initialList: string[] = Array.from({ length: 20 }, () => "");
  const [Letters, SetLetters] = useState(initialList);
  return <div>FlightItem</div>;
};
