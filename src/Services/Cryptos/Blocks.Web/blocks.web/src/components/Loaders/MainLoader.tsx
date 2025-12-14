import React from 'react';

type MainLoaderProps = {
    size?: number;
    strokeWidth?: number;
    className?: string;
};

const MainLoader: React.FC<MainLoaderProps> = ({
                                                   size = 128,
                                                   strokeWidth = 8,
                                                   className,
                                               }) => {
    const radius = (size - strokeWidth) / 2;
    const circumference = 2 * Math.PI * radius;

    return (
        <div className="d-flex justify-content-center align-items-center h-100">
            <div
                className={className}
                style={{
                    display: 'block',
                    width: size,
                    height: size,
                }}
            >
                <svg
                    width={size}
                    height={size}
                    viewBox={`0 0 ${size} ${size}`}
                    style={{ transform: 'rotate(-90deg)' }}
                >
                    <circle
                        cx={size / 2}
                        cy={size / 2}
                        r={radius}
                        stroke="#e5e7eb"
                        strokeWidth={strokeWidth}
                        fill="none"
                    />
                    <circle
                        cx={size / 2}
                        cy={size / 2}
                        r={radius}
                        stroke="#3b82f6"
                        strokeWidth={strokeWidth}
                        strokeLinecap="round"
                        strokeDasharray={circumference}
                        strokeDashoffset={circumference * 0.25}
                        fill="none"
                        style={{
                            transformOrigin: '50% 50%',
                            animation: 'main-loader-spin 1s linear infinite',
                        }}
                    />
                </svg>
                <style>
                    {`
          @keyframes main-loader-spin {
            0% {
              transform: rotate(0deg);
            }
            100% {
              transform: rotate(360deg);
            }
          }
        `}
                </style>
            </div>
        </div>
    );
};

export default MainLoader;