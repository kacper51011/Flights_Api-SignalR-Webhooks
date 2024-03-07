import { useEffect, useState } from "react";

type props = {
  letter: string;
  isLastInSentence?: boolean;
};
export const ItemLetter = ({ letter, isLastInSentence }: props) => {
  const [previousLetter, setPreviousLetter] = useState<string | null>(null);
  const [isVisible, setIsVisible] = useState(false);

  useEffect(() => {
    // At first, we want to see the animation change, then change the letter and show the changed letter
    if (previousLetter == null) {
      setPreviousLetter(letter);
      setIsVisible(true);
    }

    if (previousLetter != null && isVisible == true) {
      setIsVisible(false);
      setTimeout(() => {
        setIsVisible(true);
        setPreviousLetter(letter);
      }, 1200);
    }
  }, [letter]);

  const addedMargin: string = isLastInSentence ? "mr-2" : "";
  const animation = isVisible ? "animate-trans-down" : "animate-trans-up";
  return (
    <div
      className={
        "text-center text-xl font-semibold text-yellow-500 border-white bg-gray-800 leading-8 h-8 w-8 overflow-hidden " +
        addedMargin
      }
    >
      <div className={animation}>{previousLetter?.toUpperCase()}</div>
    </div>
  );
};
