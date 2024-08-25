export interface IconButtonProps {
    src: string;
    alt: string;
    onClick: () => void;
    size?: string;
}

function IconButton({ src, alt, onClick, size}: IconButtonProps) {
  return (
    <img src={src} alt={alt} onClick={onClick} style={
        {
            cursor: 'pointer',
            width: size || '22px',
            height: size || '22px',
        }
    }/>
  );
}
export default IconButton;