import { useState } from "react";
import { ItemLetter } from "./ItemLetter";

export const FlightItem = () => {
  const initialList: string[] = Array.from({ length: 36 }, () => "");

  const [Letters, SetLetters] = useState(initialList);
  return (
    <li className="flex py-2 gap-1">
      {Letters.map((x, index) => {
        if ([9, 19, 23, 27, 33].includes(index)) {
          return <ItemLetter letter="u" isLastInSentence />;
        } else {
          return <ItemLetter letter="x" />;
        }
      })}
    </li>
  );
};
