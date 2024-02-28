import React from "react";
import { FlightItem } from "./FlightItem";

export const FlightList = () => {
  return (
    <main className=" bg-gray-700 rounded-xl p-2 font-bold text-xl text-yellow-500">
      <ul className="flex flex-row ">
        <label className="w-location ">From</label>
        <label className="w-location">To</label>
        <label className="w-time">Start</label>
        <label className="w-time">End</label>
        <label className="w-status">Status</label>
        <label className="w-delay">Delay</label>
      </ul>
      <ul>
        <FlightItem />
        <FlightItem />
        <FlightItem />
      </ul>
    </main>
  );
};
