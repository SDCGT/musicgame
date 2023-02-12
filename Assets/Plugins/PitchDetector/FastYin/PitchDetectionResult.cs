using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PitchDetectionResult
{
    /**
     * The pitch in Hertz.
     * 用来存储音频块的音调信息的。
     * 该类有三个私有成员变量：pitch（音调，单位是赫兹），probability（一种可能性度量，如噪音程度，周期性程度等），pitched（布尔类型，表示算法认为这个音频块是否是有音调的）。
     * 这个类有两个构造函数：一个是默认构造函数，将三个成员变量设置为默认值；另一个是拷贝构造函数，可以将其他PitchDetectionResult对象的值复制过来。
     * 这个类还有一个公共clone()方法，可以复制一个PitchDetectionResult对象。这个类还提供了访问和修改三个成员变量的公共方法。
     */
    private float pitch;

    private float probability;

    private bool pitched;

    public PitchDetectionResult()
    {
        pitch = -1;
        probability = -1;
        pitched = false;
    }

    /**
     * A copy constructor. Since PitchDetectionResult objects are reused for performance reasons, creating a copy can be practical.
     * @param other
     */
    public PitchDetectionResult(PitchDetectionResult other)
    {
        this.pitch = other.pitch;
        this.probability = other.probability;
        this.pitched = other.pitched;
    }


    /**
     * @return The pitch in Hertz.
     */
    public float getPitch()
    {
        return pitch;
    }

    public void setPitch(float pitch)
    {
        this.pitch = pitch;
    }

    /* (non-Javadoc)
     * @see java.lang.Object#clone()
     */
    public PitchDetectionResult clone()
    {
        return new PitchDetectionResult(this);
    }

    /**
     * @return A probability (noisiness, (a)periodicity, salience, voicedness or
     *         clarity measure) for the detected pitch. This is somewhat similar
     *         to the term voiced which is used in speech recognition. This
     *         probability should be calculated together with the pitch. The
     *         exact meaning of the value depends on the detector used.
     */
    public float getProbability()
    {
        return probability;
    }

    public void setProbability(float probability)
    {
        this.probability = probability;
    }

    /**
     * @return Whether the algorithm thinks the block of audio is pitched. Keep
     *         in mind that an algorithm can come up with a best guess for a
     *         pitch even when isPitched() is false.
     */
    public bool isPitched()
    {
        return pitched;
    }

    public void setPitched(bool pitched)
    {
        this.pitched = pitched;
    }
}