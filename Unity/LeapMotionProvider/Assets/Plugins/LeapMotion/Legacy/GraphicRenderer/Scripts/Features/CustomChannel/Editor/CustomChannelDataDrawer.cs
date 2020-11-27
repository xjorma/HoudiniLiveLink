/******************************************************************************
 * Copyright (C) Ultraleap, Inc. 2011-2020.                                   *
 *                                                                            *
 * Use subject to the terms of the Apache License 2.0 available at            *
 * http://www.apache.org/licenses/LICENSE-2.0, or another agreement           *
 * between Ultraleap and you, your company or other organization.             *
 ******************************************************************************/

using System;
using UnityEditor;
using Leap.Unity.Query;

namespace Leap.Unity.GraphicalRenderer {

  [CustomPropertyDrawer(typeof(CustomChannelDataBase), useForChildren: true)]
  public class CustomChannelDataBaseDrawer : CustomPropertyDrawerBase {

    protected override void init(SerializedProperty property) {
      base.init(property);

      var channelFeature = LeapGraphicEditor.currentFeature as ICustomChannelFeature;
      Func<string> nameFunc = () => {
        if (channelFeature == null) {
          return null;
        } else {
          return channelFeature.channelName;
        }
      };

      drawProperty("_value", nameFunc);
    }
  }
}
