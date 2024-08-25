import { useEffect, useRef, useState } from "react";
import './NNTextArea.scss';

export interface NNTextAreaProps {
    value: string;
    onChange: (value: string) => void;
}

function NNTextArea({ value, onChange }:NNTextAreaProps) {

    const [text, setText] = useState(value);
    const textareaRef = useRef<HTMLTextAreaElement>(null);

    useEffect(() => {
        if (textareaRef.current) {
            textareaRef.current.style.height = 'auto';
            textareaRef.current.style.height = `${textareaRef.current.scrollHeight}px`;
        }
    }, [text]);

    const handleChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
        setText(e.target.value);
        onChange(e.target.value);
    };

    return <textarea 
        ref={textareaRef}
        className='nn-text-area'
        value={text} 
        onChange={handleChange} 
        />;
}
export default NNTextArea;