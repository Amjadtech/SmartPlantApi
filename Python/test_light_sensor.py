import unittest
from unittest.mock import patch, Mock
from light_sensor import validate_light_level, read_light_level, collect_light_readings


class TestLightSensor(unittest.TestCase):

    def test_negative_light_level(self):
        with self.assertRaises(ValueError):
            validate_light_level(-10)

    def test_valid_light_level(self):
        self.assertEqual(validate_light_level(500), 500)

    def test_zero_light_level(self):
        self.assertEqual(validate_light_level(0), 0)

    def test_read_light_level(self):
        self.assertEqual(read_light_level(lambda: 350), 350)

    def test_invalid_sensor_reading(self):
        with self.assertRaises(ValueError):
            read_light_level(lambda: -50)

    def test_collect_light_readings(self):
        readings = collect_light_readings(lambda: 350, 3, 0)
        self.assertEqual(readings, [350, 350, 350])

    def test_measurement_interval(self):
        with patch("light_sensor.time.sleep") as mock_sleep:
            collect_light_readings(lambda: 350, 3, 5)

            self.assertEqual(mock_sleep.call_count, 2)
            mock_sleep.assert_called_with(5)

    def test_sensor_error(self):
        def broken_sensor():
            raise OSError("Sensor connection failed")

        readings = collect_light_readings(broken_sensor, 3, 0)
        self.assertEqual(readings, [])

    def test_sensor_recovery(self):
        sensor = Mock(side_effect=[300, OSError("Sensor error"), 450])

        readings = collect_light_readings(sensor, 3, 0)

        self.assertEqual(readings, [300, 450])
        self.assertEqual(sensor.call_count, 3)

 
