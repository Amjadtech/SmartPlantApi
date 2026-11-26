import time
import random


def validate_light_level(lux):
    if lux < 0:
        raise ValueError("Light level cannot be negative")

    return lux


def read_light_level(sensor):
    lux = sensor()
    return validate_light_level(lux)


def collect_light_readings(sensor, count, interval):
    readings = []

    for i in range(count):
        try:
            lux = read_light_level(sensor)
            readings.append(lux)

        except (OSError, ValueError) as error:
            print(f"Sensor error: {error}")

        if i < count - 1:
            time.sleep(interval)

    return readings


def simulated_sensor():
    return random.randint(100, 800)


def main():
    print("SmartPlant - Light sensor started")

    while True:
        readings = collect_light_readings(simulated_sensor, 1, 0)

        for lux in readings:
            print(f"Light level: {lux} lux")

        time.sleep(5)

if __name__ == "__main__":
    main()

 