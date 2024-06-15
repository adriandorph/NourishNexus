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
                onChange={props.onChange} 
            />
        </div>
    );
}

export default SearchBar;