type props = {
  letter: string;
  isLastInSentence?: boolean;
};
export const ItemLetter = ({ letter, isLastInSentence }: props) => {
  const addedMargin: string = isLastInSentence ? "mr-2" : "";
  return (
    <div
      className={
        "text-center text-xl font-semibold text-yellow-500 border-white bg-gray-800 leading-8 h-8 w-8 " + addedMargin
      }
    >
      {letter.toUpperCase()}
    </div>
  );
};
