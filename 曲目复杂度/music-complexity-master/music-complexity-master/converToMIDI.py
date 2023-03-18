from music21 import *

# 定义xml文件路径
xml_path = "G:\学位论文\musicgame\曲目复杂度\music-complexity-master\music-complexity-master\corpus\XML/test.xml"

# 定义MIDI文件路径
midi_path = "G:\学位论文\musicgame\曲目复杂度\music-complexity-master\music-complexity-master\corpus\MIDI/test.mid"

# 读取xml文件并转换为music21 Score对象
score = converter.parse(xml_path)

# 将Score对象保存为MIDI文件
score.write('midi', midi_path)
