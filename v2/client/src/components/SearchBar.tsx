import '../styles/SearchBar.scss';


export interface SearchBarProps {
    placeholder: string;
    onChange: (e: any) => void;
}

function SearchBar(props: SearchBarProps) {
    return (
        <div className='search-bar'>
            <input 
                type='text' 
                placeholder={props.placeholder} 
                onChange={(e) => props.onChange(e.target.value)}
            />
        </div>
    );
}

export default SearchBar;