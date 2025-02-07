import '../styles/InputField.scss';

interface InputFieldProps {
    placeholder: string;
    value: string;
    onChange: (e: any) => void;
    title: string;
    type: string;
}

function InputField(props : InputFieldProps) {

    return (
        <div>
            <div className='input-title'>{props.title}</div>
            <div className='input-field'>
                <input 
                    type={props.type} 
                    value={props.value} 
                    placeholder={props.placeholder} 
                    onChange={props.onChange} 
                />
            </div>
        </div>
    );
}

export default InputField;