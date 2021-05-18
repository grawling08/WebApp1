const modal = Vue.createApp({});

 modal.component('modal-component', {
    props: ['id'],
    template: `
        <transition name="modal">
            <div class="modal-mask">
                <div class="modal-wrapper">
                    <div class="modal-container">

                        <div class="modal-header">
                            <slot name="header">
                                default header
                            </slot>
                        </div>

                        <div class="modal-body">
                            <slot name="body">
                                {{id}}
                            </slot>
                        </div>

                        <div class="modal-footer">
                            <slot name="footer">
                                default footer
                                <button class="modal-default-button" @click="$emit('close')">
                                    OK
                                </button>
                            </slot>
                        </div>
                    </div>
                </div>
            </div>
        </transition>
    `
});

export default modal;

//export default Vue.component('modal-component', {
//    props: ['id'],
//    template: `
//        <transition name="modal">
//            <div class="modal-mask">
//                <div class="modal-wrapper">
//                    <div class="modal-container">

//                        <div class="modal-header">
//                            <slot name="header">
//                                default header
//                            </slot>
//                        </div>

//                        <div class="modal-body">
//                            <slot name="body">
//                                {{id}}
//                            </slot>
//                        </div>

//                        <div class="modal-footer">
//                            <slot name="footer">
//                                default footer
//                                <button class="modal-default-button" @click="$emit('close')">
//                                    OK
//                                </button>
//                            </slot>
//                        </div>
//                    </div>
//                </div>
//            </div>
//        </transition>
//    `
//});