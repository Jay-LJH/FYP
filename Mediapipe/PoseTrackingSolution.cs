// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System.Collections;
using UnityEngine;
using MoC;
namespace Mediapipe.Unity.Sample.PoseTracking
{
  public class PoseTrackingSolution : ImageSourceSolution<PoseTrackingGraph>
  {
    private MoCManager mocManager;
    public PoseTrackingGraph.ModelComplexity modelComplexity
    {
      get => graphRunner.modelComplexity;
      set => graphRunner.modelComplexity = value;
    }

    public bool smoothLandmarks
    {
      get => graphRunner.smoothLandmarks;
      set => graphRunner.smoothLandmarks = value;
    }

    public bool enableSegmentation
    {
      get => graphRunner.enableSegmentation;
      set => graphRunner.enableSegmentation = value;
    }

    public bool smoothSegmentation
    {
      get => graphRunner.smoothSegmentation;
      set => graphRunner.smoothSegmentation = value;
    }

    public float minDetectionConfidence
    {
      get => graphRunner.minDetectionConfidence;
      set => graphRunner.minDetectionConfidence = value;
    }

    public float minTrackingConfidence
    {
      get => graphRunner.minTrackingConfidence;
      set => graphRunner.minTrackingConfidence = value;
    }

    protected override void SetupScreen(ImageSource imageSource)
    {
      base.SetupScreen(imageSource);
    }

    protected override void OnStartRun()
    {
      mocManager = MoCManager.instance;
      if (!runningMode.IsSynchronous())
      {
        graphRunner.OnPoseDetectionOutput += OnPoseDetectionOutput;
        graphRunner.OnPoseLandmarksOutput += OnPoseLandmarksOutput;
        graphRunner.OnPoseWorldLandmarksOutput += OnPoseWorldLandmarksOutput;
        graphRunner.OnSegmentationMaskOutput += OnSegmentationMaskOutput;
        graphRunner.OnRoiFromLandmarksOutput += OnRoiFromLandmarksOutput;
      }

      var imageSource = ImageSourceProvider.ImageSource;
    }

    protected override void AddTextureFrameToInputStream(TextureFrame textureFrame)
    {
      graphRunner.AddTextureFrameToInputStream(textureFrame);
    }

    protected override IEnumerator WaitForNextValue()
    {
      var task = graphRunner.WaitNextAsync();
      yield return new WaitUntil(() => task.IsCompleted);

      var result = task.Result;

      result.segmentationMask?.Dispose();
    }

    private void OnPoseDetectionOutput(object stream, OutputStream<Detection>.OutputEventArgs eventArgs)
    {
      var packet = eventArgs.packet;
      var value = packet == null ? default : packet.Get(Detection.Parser);
    }

    private void OnPoseLandmarksOutput(object stream, OutputStream<NormalizedLandmarkList>.OutputEventArgs eventArgs)
    {
      var packet = eventArgs.packet;
      var value = packet == null ? default : packet.Get(NormalizedLandmarkList.Parser);
      if(packet != null)
        mocManager.addData(value.Landmark[21].X, value.Landmark[21].Y,value.Landmark[21].Visibility, 
        value.Landmark[22].X, value.Landmark[22].Y,value.Landmark[22].Visibility);
    }

    private void OnPoseWorldLandmarksOutput(object stream, OutputStream<LandmarkList>.OutputEventArgs eventArgs)
    {
      var packet = eventArgs.packet;
      var value = packet == null ? default : packet.Get(LandmarkList.Parser);
      
    }

    private void OnSegmentationMaskOutput(object stream, OutputStream<ImageFrame>.OutputEventArgs eventArgs)
    {
      var packet = eventArgs.packet;
      var value = packet == null ? default : packet.Get();
      value?.Dispose();
    }

    private void OnRoiFromLandmarksOutput(object stream, OutputStream<NormalizedRect>.OutputEventArgs eventArgs)
    {
      var packet = eventArgs.packet;
      var value = packet == null ? default : packet.Get(NormalizedRect.Parser);
    }
  }
}
