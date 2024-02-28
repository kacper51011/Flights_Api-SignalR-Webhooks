import type { Config } from "tailwindcss";

export default {
  content: ["src/**"],
  theme: {
    extend: {
      width: {
        location: "368px",
        time: "158px",
        status: "208px",
        delay: "78px",
      },
    },
  },
  plugins: [],
} satisfies Config;
