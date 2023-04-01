import os
import mido
from music21 import *

# 设置要读取的文件夹路径和输出文件夹路径
input_folder_path = "G:\学位论文\musicgame\曲目复杂度\music-complexity-master\music-complexity-master\corpus\XML"
output_folder_path = "G:\学位论文\musicgame\曲目复杂度\music-complexity-master\music-complexity-master\corpus\MIDI"

# 循环遍历文件夹中的每个XML文件
for filename in os.listdir(input_folder_path):
    if filename.endswith(".xml"):
        xml_path = os.path.join(input_folder_path, filename)

        # 使用Music21库读取XML文件并将其转换为MIDI文件
        s = converter.parse(xml_path)
        mf = midi.translate.music21ObjectToMidiFile(s)

        # 生成输出文件的路径和文件名
        midi_filename = os.path.splitext(filename)[0] + ".mid"
        midi_path = os.path.join(output_folder_path, midi_filename)

        # 保存MIDI文件到输出文件夹
        mf.open(midi_path, 'wb')
        mf.write()
        mf.close()


