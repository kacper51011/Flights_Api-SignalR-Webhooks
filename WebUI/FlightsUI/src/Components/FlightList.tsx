import { useEffect, useState } from "react";
import { FlightItem } from "./FlightItem";
import { HttpTransportType, HubConnection, HubConnectionBuilder } from "@microsoft/signalr";

type Flight = {
  flightId: string;
  letters: string;
};

export const FlightList = () => {
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const [flights, setFlights] = useState<Flight[]>([]);

  useEffect(() => {
    const conn = new HubConnectionBuilder()
      .withUrl("https://localhost:8001/flightshub", {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets,
      })
      .withAutomaticReconnect()
      .build();

    setConnection(conn);
  }, []);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then(() => {
          console.log("connected to hub");
        })
        .catch((error) => {
          console.log(error);
        });

      connection.on("Initialize", (flightList: Flight[]) => {
        setFlights(flightList);
      });

      connection.on("ModifyFlight", (modifiedFlight: Flight) => {
        setFlights((prevFlights) =>
          prevFlights.map((x) => (x.flightId == modifiedFlight.flightId ? modifiedFlight : x))
        );
      });

      connection.on("DeleteFlight", (deletedFlightId: string) => {
        setFlights((prevFlights) => prevFlights.filter((x) => x.flightId != deletedFlightId));
      });

      connection.on("AddFlight", (flightToAdd: Flight) => {
        setFlights((prevFlights) => [...prevFlights, flightToAdd]);
      });
    }
    return () => {
      connection?.stop();
    };
  }, [connection]);

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
        {flights.map((f) => (
          <FlightItem key={f.flightId} letters={f.letters} />
        ))}
      </ul>
    </main>
  );
};
