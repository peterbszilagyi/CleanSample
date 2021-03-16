import { FaTimes } from 'react-icons/fa'
import { FaTrashAlt } from 'react-icons/fa'
import { FaEdit } from 'react-icons/fa'
import { FaSave } from 'react-icons/fa'
import { useState } from 'react'

const Airplane = ({ airplane, onDelete, onModify }) => {

    const [inEditMode, setInEditMode] = useState(false)
    const [airplaneName, setAirplaneName] = useState(airplane)

    const onSubmit = (e) => {
        e.preventDefault();

        if (!airplaneName) {
            alert('Please provide airplane name');
        }
        setInEditMode(!inEditMode);
        onModify({ id: airplane.id, name: airplaneName });
    }


    return (
        <div className='airplane'>

            {inEditMode ?
                (
                    <form onSubmit={onSubmit}>
                        <h3>Name:
                            <input type='text' placeholder={airplane.name} onChange={(e) => setAirplaneName(e.target.value)} />
                            
                            <button type='submit'>
                                <FaSave style={{ cursor: 'pointer' }} />
                            </button>
                        </h3>
                        <h3>
                            Id: {airplane.id}
                            <FaTimes onClick={() => setInEditMode(!inEditMode)} style={{ cursor: 'pointer' }} />
                        </h3>
                    </form>
                )
                : (
                    <>
                        <h3>
                            Name: {airplane.name}
                            <FaTrashAlt onClick={() => onDelete(airplane.id)} style={{ color: 'red', cursor: 'pointer' }} />
                        </h3>
                        <h3>
                            Id: {airplane.id}
                            <FaEdit onClick={() => setInEditMode(!inEditMode)} style={{ cursor: 'pointer' }} />
                        </h3>
                    </>)}
        </div>

    )
}

export default Airplane
