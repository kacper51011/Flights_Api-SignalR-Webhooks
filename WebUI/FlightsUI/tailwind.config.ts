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
      keyframes: {
        "trans-up": {
          "0%": { transform: "translateY(0px)" },
          "100%": { transform: "translateY(-200px)" },
        },
        "trans-down": {
          "0%": { transform: "translateY(-200px)" },
          "100%": { transform: "translateY(0px)" },
        },
      },
      animation: {
        "trans-up": "trans-up 1.5s ease-in-out",
        "trans-down": "trans-down 1.5s ease-in-out",
      },
    },
  },
  plugins: [],
} satisfies Config;
