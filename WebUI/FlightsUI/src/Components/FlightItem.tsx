import { useEffect, useState } from "react";
import { ItemLetter } from "./ItemLetter";

type props = {
  letters?: string;
};

export const FlightItem = ({ letters }: props) => {
  useEffect(() => {
    if (letters) {
      const lettersArray = letters!.split("");
      if (lettersArray.length == displayedLetters.length) {
        setLetters(lettersArray);
      }
    }
  }, [letters]);

  const [displayedLetters, setLetters] = useState("Warsaw    Warsaw    14261528start 14".split(""));
  return (
    <li className="flex py-2 gap-1">
      {displayedLetters.map((x, index) => {
        if ([9, 19, 23, 27, 33].includes(index)) {
          return <ItemLetter key={index} letter={x} isLastInSentence />;
        } else {
          return <ItemLetter key={index} letter={x} />;
        }
      })}
    </li>
  );
};
