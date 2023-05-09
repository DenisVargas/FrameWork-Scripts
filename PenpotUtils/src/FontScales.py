MINOR_SECOND = 1.067
MAYOR_SECOND = 1.125
MINOR_THIRD =  1.200
MAYOR_THIRD =  1.250
PERFECT_FOUR =  1.333
AUGMENTED_FOUR =  1.414
PERFECT_FIFTH =  1.5
GOLDEN_RATIO =  1.618

def calculate_typography_scale(base, factor, up, down):
    scale = []
    scalar1 = base
    for n in range(down):
        scalar1 = scalar1 * (1/factor)
        scale.insert(0, scalar1)

    scale.append(base)
    scalar2 = base
    for m in range(up):
        scalar2 = scalar2 * factor
        scale.append(scalar2)

    rounded_scale = []
    for r in scale:
        rounded = round(r,2)
        rounded_scale.append(rounded)
    return (base, rounded_scale)

result = calculate_typography_scale(16, MINOR_THIRD, 5, 3)
# print(result)

def convert_scale_from_px_to_rem(scale):
    base, elements = scale
    converted_scale = []
    for element in elements:
        converted_value = element/base
        converted_scale.append(round(converted_value,2))
    return (1, converted_scale)

convertedToRem = convert_scale_from_px_to_rem(result)
# print(convertedToRem)