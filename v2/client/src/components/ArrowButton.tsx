import '../styles/ArrowButton.scss';

export interface ArrowButtonProps {
    onClick: () => void;
    direction: 'left' | 'right';
}

function ArrowButton(props: ArrowButtonProps) {
    return (
        <div
            className={`arrow-button ${props.direction}`}
            onClick={props.onClick}
        >
            
            <div className='triangle'></div>
            <div className='rectangle'></div>
        </div>
    );
}

export default ArrowButton;