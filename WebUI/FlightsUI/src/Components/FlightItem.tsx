import { useEffect, useState } from "react";
import { ItemLetter } from "./ItemLetter";

type props = {
  letters: string;
};

export const FlightItem = ({ letters }: props) => {
  // useEffect(() => {
  //   if (letters) {
  //     const lettersArray = letters!.split("");
  //     if (lettersArray.length == displayedLetters.length) {
  //       setLetters(lettersArray);
  //     }
  //   }
  // }, [letters]);

  return (
    <li className="flex py-2 gap-1">
      {letters.split("").map((x, index) => {
        if ([9, 19, 23, 27, 33].includes(index)) {
          return <ItemLetter key={index} letter={x} isLastInSentence />;
        } else {
          return <ItemLetter key={index} letter={x} />;
        }
      })}
    </li>
  );
};
