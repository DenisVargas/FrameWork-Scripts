import json
import os

def load_json_file(file_path):
    if os.path.isfile(file_path):
        try:
            with open(file_path) as file:
                data = json.load(file)
            return data
        except Exception as e:
            raise RuntimeError(f"Error al cargar el archivo JSON: {e}")
    else:
        raise FileNotFoundError("El archivo JSON no existe.")


def save_json_file(file_path, data):
    directory = os.path.dirname(file_path)
    if not os.path.exists(directory):
        os.makedirs(directory)

    try:
        with open(file_path, 'w') as file:
            json.dump(data, file, indent=4)
    except Exception as e:
        raise RuntimeError(f"Error al guardar el archivo JSON: {e}")
