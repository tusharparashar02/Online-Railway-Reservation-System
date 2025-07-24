// tailwind.config.js
module.exports = {
  content: [
    "./src/**/*.{html,ts}", // for Angular templates
  ],
  theme: {
    extend: {
      animation: {
        'fade-in': 'fadeIn 1s ease-out',
        'move-left': 'moveLeft 3s linear infinite',
      },
      keyframes: {
        fadeIn: {
          '0%': { opacity: 0 },
          '100%': { opacity: 1 }
        },
        moveLeft: {
          '0%': { transform: 'translateX(100%)' },
          '100%': { transform: 'translateX(-100%)' }
        }
      }
    }
  },
  plugins: [],
}