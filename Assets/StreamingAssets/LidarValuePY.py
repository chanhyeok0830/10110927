import socket
import time
import math
import ydlidar
from ydlidar import LaserScan

def send_lidar_data():
    udp_ip = "192.168.0.17"  # Unity가 실행 중인 컴퓨터의 IP 주소
    udp_port = 5005          # Unity에서 받을 포트 번호

    # LiDAR 장치 초기화
    lidar = ydlidar.CYdLidar()
    port = "COM5"  # 사용 중인 LiDAR 포트로 변경 (Windows의 경우 'COM5'와 같은 포트를 사용)
    
    lidar.setlidaropt(ydlidar.LidarPropSerialPort, port)
    lidar.setlidaropt(ydlidar.LidarPropSerialBaudrate, 128000)
    lidar.setlidaropt(ydlidar.LidarPropLidarType, ydlidar.TYPE_TRIANGLE)
    lidar.setlidaropt(ydlidar.LidarPropDeviceType, ydlidar.YDLIDAR_TYPE_SERIAL)
    lidar.setlidaropt(ydlidar.LidarPropSampleRate, 5)
    lidar.setlidaropt(ydlidar.LidarPropSingleChannel, True)
    lidar.setlidaropt(ydlidar.LidarPropMaxAngle, 180.0)  # 스캔할 최대 각도 (0-180도)
    lidar.setlidaropt(ydlidar.LidarPropMinAngle, -180.0) # 스캔할 최소 각도 (-180-0도)
    lidar.setlidaropt(ydlidar.LidarPropMaxRange, 2.0)    # 최대 감지 범위 2미터
    lidar.setlidaropt(ydlidar.LidarPropMinRange, 0.1)    # 최소 감지 범위 0.1미터

    if not lidar.initialize():
        print("Failed to initialize YDLidar")
        return

    retry_count = 0
    max_retries = 5
    while retry_count < max_retries:
        if lidar.turnOn():
            print("Lidar successfully turned on")
            break
        else:
            print(f"Failed to turn on YDLidar, retrying... ({retry_count + 1}/{max_retries})")
            retry_count += 1
            time.sleep(2)  # 2초 대기 후 재시도
    else:
        print("Exceeded maximum retries to turn on YDLidar")
        return

    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

    try:
        while True:
            scan = LaserScan()
            if lidar.doProcessSimple(scan):
                data_points = []
                for point in scan.points:
                    angle_degrees = math.degrees(point.angle)
                    if angle_degrees < 0:
                        angle_degrees += 360  # 음수 각도를 양수로 변환
                    if 90 <= angle_degrees <= 270 and 0.1 < point.range <= 1.0:
                        data_points.append((angle_degrees, point.range))
                
                # 데이터 포인트 개수
                num_points = len(data_points)
                print(f"Number of data points: {num_points}")

                # 데이터를 콘솔에 출력
                for angle, distance in data_points:
                    print(f"Angle: {angle:.2f} degrees, Distance: {distance:.2f} meters")
                
                message = str(data_points)  # 데이터를 문자열로 변환
                sock.sendto(message.encode(), (udp_ip, udp_port))
                time.sleep(0.1)  # 0.1초마다 데이터 전송
    except KeyboardInterrupt:
        pass
    finally:
        lidar.turnOff()
        lidar.disconnecting()

if __name__ == "__main__":
    send_lidar_data()
