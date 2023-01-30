using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMDF
{
	private static readonly double DEFAULT_MIN_FREQUENCY = 82.0;
	private static readonly double DEFAULT_MAX_FREQUENCY = 1000.0;
	private static readonly double DEFAULT_RATIO = 5.0;
	private static readonly double DEFAULT_SENSITIVITY = 0.1;

	private readonly float sampleRate;

	private readonly double[] amd;
	private readonly long maxPeriod;
	private readonly long minPeriod;
	private readonly double ratio;
	private readonly double sensitivity;

	/**
	 *
	 * The result of the pitch detection iteration.
	 */
	private readonly PitchDetectionResult result;

	/**
	 * Construct a new Average Magnitude Difference pitch detector.
	 *
	 * @param sampleRate The audio sample rate
	 * @param bufferSize the buffer size of a block of samples
	 */
	public AMDF(float sampleRate, int bufferSize)
		:this(sampleRate, bufferSize, DEFAULT_MIN_FREQUENCY, DEFAULT_MAX_FREQUENCY)
	{
	}

	/**
	 * Construct a new Average Magnitude Difference pitch detector.
	 *
	 * @param sampleRate The audio sample rate
	 * @param bufferSize the buffer size of a block of samples
	 * @param minFrequency The min frequency to detect in Hz
	 * @param maxFrequency The max frequency to detect in Hz
	 */
	public AMDF(float sampleRate, int bufferSize, double minFrequency, double maxFrequency)
	{
		this.sampleRate = sampleRate;
		amd = new double[bufferSize];
		this.ratio = DEFAULT_RATIO;
		this.sensitivity = DEFAULT_SENSITIVITY;
		this.maxPeriod = (long)Mathf.Round((float)(sampleRate / minFrequency + 0.5));
		this.minPeriod = (long)Mathf.Round((float)(sampleRate / maxFrequency + 0.5));
		result = new PitchDetectionResult();
	}

	public PitchDetectionResult getPitch(float[] audioBuffer)
	{
		int t = 0;
		float f0 = -1;
		double minval = double.PositiveInfinity;
		double maxval = double.NegativeInfinity;
		double[] frames1 = new double[0];
		double[] frames2 = new double[0];
		double[] calcSub = new double[0];

		int maxShift = audioBuffer.Length;


		for (int m = 0; m < maxShift; m++)
		{
			frames1 = new double[maxShift - m + 1];
			frames2 = new double[maxShift - m + 1];
			t = 0;
			for (int aux1 = 0; aux1 < maxShift - m; aux1++)
			{
				t = t + 1;
				frames1[t] = audioBuffer[aux1];

			}
			t = 0;
			for (int aux2 = m; aux2 < maxShift; aux2++)
			{
				t = t + 1;
				frames2[t] = audioBuffer[aux2];
			}

			int frameLength = frames1.Length;
			calcSub = new double[frameLength];
			for (int u = 0; u < frameLength; u++)
			{
				calcSub[u] = frames1[u] - frames2[u];
			}

			double summation = 0;
			for (int l = 0; l < frameLength; l++)
			{
				summation += Mathf.Abs((float)calcSub[l]);
			}
			amd[m] = summation;
		}

		for (int k = (int)minPeriod; k < (int)maxPeriod; k++)
		{
			if (amd[k] < minval)
			{
				minval = amd[k];
			}
			if (amd[k] > maxval)
			{
				maxval = amd[k];
			}
		}
		int cutoff = (int)Mathf.Round((float)((sensitivity * (maxval - minval)) + minval));
		int j = (int)minPeriod;

		while (j <= (int)maxPeriod && (amd[j] > cutoff))
		{
			j = j + 1;
		}

		double search_length = minPeriod / 2;
		minval = amd[j];
		int minpos = j;
		int i = j;
		while ((i < j + search_length) && (i <= maxPeriod))
		{
			i = i + 1;
			if (amd[i] < minval)
			{
				minval = amd[i];
				minpos = i;
			}
		}

		if (Mathf.Round((float)(amd[minpos] * ratio)) < maxval)
		{
			f0 = sampleRate / minpos;
		}

		result.setPitch(f0);
		result.setPitched(-1 != f0);
		result.setProbability(-1);

		return result;
	}
}
