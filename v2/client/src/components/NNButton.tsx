import { useState } from 'react';
import '../styles/NNButton.scss';

interface NNButtonProps {
    onClick?: () => void;
    text: string;
    color: string;
    textColor: string;
    sizePX: number;
    type?: 'button' | 'submit';
}

function NNButton(props: NNButtonProps) {
    const [isHovered, setIsHovered] = useState(false);

    function handleMouseEnter() {
        setIsHovered(true);
    }

    function handleMouseLeave() {
        setIsHovered(false);
    }

    return (
        <>
            <button 
                className="nnbutton"
                type={props.type || 'button'}
                style={{
                    backgroundColor: props.color, 
                    color: props.textColor,
                    fontSize: `${props.sizePX}px`,
                    borderRadius: `${props.sizePX}px`,
                    padding: `${props.sizePX / 3}px ${props.sizePX / 0.75}px`,
                    boxShadow: isHovered ? `0px 0px ${props.sizePX / 4}px ${props.sizePX / 16}px ${props.color}80` : 'none'
                }} 
                onClick={props.onClick}
                onMouseEnter={handleMouseEnter}
                onMouseLeave={handleMouseLeave}
            >
                {props.text}
            </button>
        </>
    );
}

export default NNButton;