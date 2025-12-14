#!/bin/sh

# Generate config.js with environment variables at runtime
cat <<EOF > /usr/share/nginx/html/config.js
window.ENV = {
  VITE_API_URL: '${VITE_API_URL:-https://localhost:6071}'
};
EOF

# Start nginx
exec nginx -g 'daemon off;'
