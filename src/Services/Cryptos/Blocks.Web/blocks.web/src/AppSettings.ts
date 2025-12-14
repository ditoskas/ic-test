declare global {
  interface Window {
    ENV?: {
      VITE_API_URL?: string;
    };
  }
}

export default class AppSettings {
  static ApiUrl = window.ENV?.VITE_API_URL || import.meta.env.VITE_API_URL || 'https://localhost:6071';
}