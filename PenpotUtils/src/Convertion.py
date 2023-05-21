class Rect:
    def __init__(self, width, height, name=None, id=None, zindex=None):
        self.width = width
        self.height = height
        self.name = name
        self.id = id
        self.zindex = zindex


def convertir_a_porcentaje_dict(referencia, elemento):
    referencia_width = referencia['width']
    referencia_height = referencia['height']
    elemento_width_px = elemento['width']
    elemento_height_px = elemento['height']

    elemento_width_porcentaje = round((elemento_width_px / referencia_width) * 100, 2)
    elemento_height_porcentaje = round((elemento_height_px / referencia_height) * 100, 2)

    return {
        'width': elemento_width_porcentaje,
        'height': elemento_height_porcentaje
    }

def convertir_a_porcentaje_rect(referencia, elemento):
    referencia_width = referencia.width
    referencia_height = referencia.height
    elemento_width_px = elemento.width
    elemento_height_px = elemento.height

    elemento_width_porcentaje = round((elemento_width_px / referencia_width) * 100, 2)
    elemento_height_porcentaje = round((elemento_height_px / referencia_height) * 100, 2)

    return Rect(elemento_width_porcentaje, elemento_height_porcentaje, elemento.name, elemento.id, elemento.zindex)
