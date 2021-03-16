import { useState } from 'react'

const AddAirplane = ({ onAdd }) => {

    const [name, setName] = useState('')

    const onSubmit = (e) => {
        e.preventDefault();

        if (!name) {
            alert('Please provide airplane name');
        }

        onAdd({ name })
        setName('')
    }

    return (
        <form className='add-form' onSubmit={onSubmit}>
            <div className='form-control'>
                <label>Airplane</label>
                <input type='text'
                    placeholder='The name of the new airplane'
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                />
            </div>
            <input type='submit' value='Save' className='btn btn-block' />
        </form>
    )
}


export default AddAirplane