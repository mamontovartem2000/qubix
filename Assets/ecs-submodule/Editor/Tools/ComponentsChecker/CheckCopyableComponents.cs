using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace ME.ECSEditor.Tools {

    public class CheckCopyableComponents : EditorWindow {

        [MenuItem("ME.ECS/Tools/Copyable Components Checker...")]
        public static void ShowWindow() {
            
            var window = CheckCopyableComponents.CreateInstance<CheckCopyableComponents>();
            window.titleContent = new UnityEngine.GUIContent("Copyable Components Checker");
            window.Show();
            
        }

        private TestsView view;
        
        public void OnEnable() {
            
            var container = new VisualElement();
            container.styleSheets.Add(EditorUtilities.Load<StyleSheet>("Editor/Tools/Styles.uss", isRequired: true));

            var view = new TestsView(() => {

                var collectedComponents = new List<TestItem>();
                var asms = System.AppDomain.CurrentDomain.GetAssemblies();
                foreach (var asm in asms) {

                    var types = asm.GetTypes();
                    foreach (var type in types) {

                        if (type.IsValueType == true && typeof(ME.ECS.ICopyableBase).IsAssignableFrom(type) == true) {
                        
                            collectedComponents.Add(new TestItem() {
                                type = type,
                                tests = new [] {
                                    new TestInfo(TestMethod.CopyFrom),
                                    new TestInfo(TestMethod.Recycle), 
                                },
                            });
                            
                        }

                    }

                }
                
                return collectedComponents.OrderBy(x => x.type.Name).ToList();

            }, true);
            this.view = view;
            container.Add(view);
            
            this.rootVisualElement.Add(container);
            
        }

        public void Update() {

            this.view?.Update();

        }

    }

}