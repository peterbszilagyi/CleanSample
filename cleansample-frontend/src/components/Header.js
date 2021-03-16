import PropTypes from 'prop-types'
import Button from './Button'


const Header = ({ title, onAdd, showAdd }) => {

    const onClick = () => {
        onAdd();
    }

    return (
        <header className='header'>
            <h1>{title}</h1>
            <Button color={showAdd ? 'gray' : 'green'} text={showAdd ? 'Close' : 'Add'} onClick={onClick}></Button>
        </header>
    )
}


Header.defaultProps = {
    title: 'No title provided',
}

Header.propTypes = {
    title: PropTypes.string
}

export default Header
