import json
from Convertion import Rect, convertir_a_porcentaje_rect
from JsonFile import load_json_file, save_json_file

def main():
    # Cargar archivo JSON
    try:
        data = load_json_file('./inputs/input.json')
    except (FileNotFoundError, RuntimeError) as e:
        print(f"Error: {e}")
        return

    # Obtener el objeto Frame de referencia
    referencia_data = data['frame']
    referencia = Rect(referencia_data['width'], referencia_data['height'])

    # Obtener la lista de elementos en root
    elementos = data['root']

    # Crear una lista para almacenar los resultados de conversión
    resultados = []

    # Convertir cada elemento a instancias de la clase Rect y realizar la conversión a porcentaje
    for elemento in elementos:
        # print(elemento)
        name = elemento.get('name')
        id = elemento.get('id')
        zindex = elemento.get('zindex')
        width = elemento.get('width')
        height = elemento.get('height')
        rect = Rect(width, height, name, id, zindex)
        resultado = convertir_a_porcentaje_rect(referencia, rect)
        resultados.append(resultado.__dict__)

    # Actualizar el objeto JSON con los resultados de conversión
    data['root'] = resultados

    # Guardar el resultado en un nuevo archivo JSON
    try:
        save_json_file('./outputs/output.json', data)
        print("Resultado guardado en el archivo 'output.json'.")
    except RuntimeError as e:
        print(f"Error al guardar el archivo JSON: {e}")


if __name__ == '__main__':
    main()
