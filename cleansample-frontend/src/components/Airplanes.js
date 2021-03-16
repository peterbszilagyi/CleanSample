import Airplane from './Airplane'

const Airplanes = ({ airplanes, onDelete, onModify }) => {
    return (
        <>
            {airplanes.map((airplane) =>
            (
                <Airplane key={airplane.id}
                    airplane={airplane}
                    onDelete={onDelete}
                    onModify={onModify} />
            )
            )}
        </>
    )
}

export default Airplanes
