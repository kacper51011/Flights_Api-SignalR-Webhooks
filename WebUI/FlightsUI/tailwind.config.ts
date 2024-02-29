import type { Config } from "tailwindcss";

export default {
  content: ["src/**"],
  theme: {
    extend: {
      width: {
        location: "368px",
        time: "154px",
        status: "216px",
        delay: "78px",
      },
    },
  },
  plugins: [],
} satisfies Config;
