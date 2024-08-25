import pytest
from src.JsonFile import load_json_file
from src.IndexElement import DOElement

def test_cast_to_IndexElement():
    # Cargar datos del archivo JSON en su ruta correspondiente.
    data = load_json_file('inputs/index.json')
    print("Loaded json File is:")
    print(data)
    # Llamar a la función y obtener el resultado
    print("printing items in inside data")
    results = []
    for item in data['index']:
        result = DOElement.from_json(item)
        results.append(result)

    # Verificar el resultado esperado
    assert isinstance(results[0], DOElement)
    assert results[0].item == "Title1"
    assert results[0].page == 1
    assert len(results[0].children) == 1
    assert results[0].children[0].item == "SubTitle1"
    assert results[0].children[0].page == 3

    assert isinstance(results[1], DOElement)
    assert results[1].item == "Title2"
    assert results[1].page == 10
    assert len(results[1].children) == 0

    # Imprimir el resultado por consola
    # print(result.item)
    # print(result.page)
    # for child in result.children:
    #     print(child.item)
    #     print(child.page)
