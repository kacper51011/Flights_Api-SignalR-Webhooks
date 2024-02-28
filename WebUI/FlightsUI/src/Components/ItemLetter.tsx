import React from "react";

type props = {
  letter: string;
};
export const ItemLetter = ({ letter }: props) => {
  return (
    <div className=" text-center  text-2xl font-semibold text-slate-100 border-white bg-gray-800 leading-10 h-10 w-10">
      {letter.toUpperCase()}
    </div>
  );
};
