# StationJ
   - 제물포 역을 기준으로 지도를 생성하여 유저가 제물포 주변에 있는 시설들을 잘 이용 할 수 있도록 AR Navigation, PhotoZone 등, 다양한 정보를 제공하는 Android Application입니다.
   - AR기술을 활용하여 네비게이션 사용 시 실시간으로 바닥에 화살표를 띄워 유저에게 직관적으로 길을 알려주거나, 포토존이나 특정 랜드마크에 가면 3D 오브젝트와 촬영을 하고, 해당 랜드마크의 설명을 위해 3D 캐릭터가 등장해 설명을 해주는 등 각종 상호작용도 할 수 있도록 기획 되었습니다.

## 상태 및 전제조건
> Unity Version : Unity 22.3.2f


> Platform : Android (Minimum Android Version: Android 7.0 'Nougat')

## 시작하는 방법
### ARCore Extension Package 사용법

### Google Cloud Platform API 사용법

### Geospatial API 사용방법
#### 사용 설정 및 API Key 사용 설명

### Geospatial Creator API 사용 방법
#### Cesium Package 설치

### Naver API 사용 설명
#### 1. Static Map API
#### 2. Directions 5 API
   - API Key

## 해당 프로젝트를 사용하여 개발
### 1. POI json 구조
```
{
	"pois": [
		{
			"name": "cafe 07 am",
			"type": "화장실",
			"image": **base64형식의 이미지파일**,
			"latitude": 37.714853,
			"longitude": 126.741614,
			"address": "경기 파주시 와석순환로192번길 14-38 1층 카페.07.am",
			"description": "영업시간: 09시~20시",
			"coupon": "커피 10% 할인"
		},
		{
			"name": "더브래드36.5도",
			"type": "포토존",
			"image": **base64형식의 이미지파일**,
			"latitude": 37.714980,
			"longitude": 126.741729,
			"address": "경기 파주시 와석순환로192번길 14-43",
			"description": "영업시간: 09시~20시",
			"coupon": "식사 10% 할인"
		},
  ...
 ]
}
```
### 2. Static Map API 링크

### 3. Directions 5 json 구조
```
{
	"code":0,
	"message":"길찾기를 성공하였습니다.",
	"currentDateTime":"2024-02-25T22:44:01",
	"route":{
		"traoptimal":[{
			"summary":{
				"start":{
					"location":[126.7435712,37.7136746]
				},
				"goal":{
					"location":[126.7416992,37.7146793],
					"dir":2
				},
				"distance":388,
				"duration":124281,
				"etaServiceType":0,
				"departureTime":"2024-02-25T22:44:01",
				"bbox":[
					[126.7408252,37.7136746],
					[126.7436509,37.7148058]
				],
				"tollFare":0,
				"taxiFare":4800,
				"fuelPrice":45
			},
			"path":[
				[126.7436509,37.7138734],
				[126.7436452,37.7138752]
			],
			"section":[{
				"pointIndex":6,
				"pointCount":7,
				"distance":107,
				"name":"와석순환로172번길",
				"congestion":0,"speed":11}
			],
			"guide":[{
				"pointIndex":6,
				"type":2,
				"instructions":"'와석순환로172번길' 방면으로로 좌회전",
				"distance":187,
				"duration":52745
			}
			{
				"pointIndex":18,
				"type":88,
				"instructions":"목적지",
				"distance":64,
				"duration":28799
			}
			]
		}
		]
	}
}
```
## License
### License.md 
