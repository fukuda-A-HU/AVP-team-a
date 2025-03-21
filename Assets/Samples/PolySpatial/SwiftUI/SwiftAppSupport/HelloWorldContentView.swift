//
// This custom View is referenced by SwiftUISampleInjectedScene
// to provide the body of a WindowGroup. It's part of the Unity-VisionOS
// target because it lives inside a "SwiftAppSupport" directory (and Unity
// will move it to that target).
//

import Foundation
import SwiftUI
import UnityFramework
import PolySpatialRealityKit

struct HelloWorldContentView: View {
    @State var counterObject = ObjectCounter()
    @State var fps = Float(90)

    var body: some View {
        VStack {
            Text("Hello, SwiftUI!")
            Divider()
                .padding(10)
            Text(String(format: "Unity Simulation FPS: %.1f", fps))
            Button("Spawn Red Object") {
                CallCSharpCallback("spawn red")
                UpdateValues(counter: counterObject)
            }
            Button("Spawn Green Object") {
                CallCSharpCallback("spawn green")
                UpdateValues(counter: counterObject)
            }
            Button("Spawn Blue Object") {
                CallCSharpCallback("spawn blue")
                UpdateValues(counter: counterObject)
            }
            Button("Make It Pink") {
                let instanceId = GetLastObjectInstanceID()
                let entities = PolySpatialWindowManagerAccess.entitiesForUnityInstanceId(id: instanceId)
                for entity in entities {
                    if let identifier = PolySpatialWindowManagerAccess.identifierForEntity(entity: entity) {
                        CallCSharpCallback("recolor", identifier.unityInstanceId)
                    }
                }
            }.disabled(counterObject.cubeCount == 0 && counterObject.sphereCount == 0)
        }
        .onAppear {
            // Call the public function that was defined in SwiftUISamplePlugin
            // inside UnityFramework
            CallCSharpCallback("appeared")
            UpdateValues(counter: counterObject)
            SubscribeToSetFPS() { fps in
                self.fps = fps
            }
        }
        .onDisappear {
            CallCSharpCallback("closed")
        }

        // Changes to counterObject will result in updates to these text views
        HStack {
            Text("Cube Count: \(counterObject.cubeCount)")
                .padding(5)
            Text("Sphere Count: \(counterObject.sphereCount)")
                .padding(5)
        }
        .padding(10)
    }
}

func UpdateValues(counter: ObjectCounter) {
    counter.cubeCount = GetCubeCount()
    counter.sphereCount = GetSphereCount()
}

#Preview(windowStyle: .automatic) {
    HelloWorldContentView()
}
